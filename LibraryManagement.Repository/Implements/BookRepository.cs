using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implements
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public int CountAll()
        {
            return _context.Books.Count();
        }

        public int CountAvailableCopies()
        {
            return _context.BookCopies.Count(c => c.IsAvailable);
        }

        public int CountCopies()
        {
            return _context.BookCopies.Count();
        }

        public int CountEditions()
        {
            return _context.BookEditions.Count();
        }

        public void Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return;
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Include(b => b.Editions)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Include(b => b.Editions).ThenInclude(e => e.Copies)
                .FirstOrDefault(b => b.Id == id);
        }

        public List<(string Title, string? CoverImage, int BorrowCount)> GetTopBorrowed(int top = 5)
        {
            return _context.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.BookEdition)
                    .ThenInclude(e => e.Book)
                .GroupBy(l => new
                {
                    l.BookCopy.BookEdition.Book.Id,
                    l.BookCopy.BookEdition.Book.Title,
                    l.BookCopy.BookEdition.Book.CoverImage
                })
                .Select(g => new
                {
                    g.Key.Title,
                    g.Key.CoverImage,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(top)
                .AsEnumerable()
                .Select(x => (x.Title, x.CoverImage, x.BorrowCount))
                .ToList();
        }

        public void Update(Book book)
        {
            var existing = _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookCategories)
                .FirstOrDefault(b => b.Id == book.Id);

            if (existing == null) return;

            existing.Title = book.Title;
            existing.Description = book.Description;
            existing.CoverImage = book.CoverImage;

            // Cập nhật Authors
            existing.BookAuthors.Clear();
            foreach (var ba in book.BookAuthors)
                existing.BookAuthors.Add(ba);

            // Cập nhật Categories
            existing.BookCategories.Clear();
            foreach (var bc in book.BookCategories)
                existing.BookCategories.Add(bc);

            _context.SaveChanges();
        }
    }
}
