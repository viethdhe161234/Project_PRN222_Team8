using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.DTO;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public int CountAll()
        {
            return _bookRepository.CountAll();
        }

        public int CountAvailableCopies()
        {
            return _bookRepository.CountAvailableCopies();
        }

        public int CountCopies()
        {
            return _bookRepository.CountCopies();
        }

        public int CountEditions()
        {
            return _bookRepository.CountEditions();
        }

        public void Create(string title, string? description, string? coverImage, List<int> authorIds, List<int> categoryIds)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title is required");

            if (!authorIds.Any())
                throw new Exception("At least one author is required");

            if (!categoryIds.Any())
                throw new Exception("At least one category is required");

            var book = new Book
            {
                Title = title,
                Description = description,
                CoverImage = coverImage,
                BookAuthors = authorIds.Select(id => new BookAuthor { AuthorId = id }).ToList(),
                BookCategories = categoryIds.Select(id => new BookCategory { CategoryId = id }).ToList()
            };

            _bookRepository.Add(book);
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public List<BookBorrowStat> GetTopBorrowed(int top = 5)
        {
            return _bookRepository.GetTopBorrowed(top)
                .Select(x => new BookBorrowStat
                {
                    Title = x.Title,
                    CoverImage = x.CoverImage,
                    BorrowCount = x.BorrowCount
                })
                .ToList();
        }

        public void Update(int id, string title, string? description, string? coverImage, List<int> authorIds, List<int> categoryIds)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title is required");

            if (!authorIds.Any())
                throw new Exception("At least one author is required");

            if (!categoryIds.Any())
                throw new Exception("At least one category is required");

            var book = new Book
            {
                Id = id,
                Title = title,
                Description = description,
                CoverImage = coverImage,
                BookAuthors = authorIds.Select(aid => new BookAuthor { BookId = id, AuthorId = aid }).ToList(),
                BookCategories = categoryIds.Select(cid => new BookCategory { BookId = id, CategoryId = cid }).ToList()
            };

            _bookRepository.Update(book);
        }
    }
}
