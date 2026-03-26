using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implements
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly LibraryDbContext _context;
        public BookCopyRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(BookCopy copy)
        {
            _context.BookCopies.Add(copy);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var copy = _context.BookCopies.FirstOrDefault(c => c.Id == id);
            if (copy == null) return;

            _context.BookCopies.Remove(copy);
            _context.SaveChanges();
        }

        public bool ExistsByBarcode(string barcode)
        {
            return _context.BookCopies.Any(c => c.Barcode == barcode);
        }

        public BookCopy? GetAvailableByEditionId(int editionId)
        {
            return _context.BookCopies
                .FirstOrDefault(c => c.BookEditionId == editionId && c.IsAvailable);
        }

        public IEnumerable<BookCopy> GetByEditionId(int editionId)
        {
            return _context.BookCopies
                .Where(c => c.BookEditionId == editionId)
                .ToList();
        }

        public BookCopy? GetById(int id)
        {
            return _context.BookCopies.FirstOrDefault(c => c.Id == id);
        }

        public void Update(BookCopy copy)
        {
            var existing = _context.BookCopies.FirstOrDefault(c => c.Id == copy.Id);
            if (existing == null) return;

            existing.IsAvailable = copy.IsAvailable;
            _context.SaveChanges();
        }
    }
}
