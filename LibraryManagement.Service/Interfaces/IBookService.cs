using LibraryManagement.Domain.Entities;
using LibraryManagement.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        void Create(string title, string? description, string? coverImage,
            List<int> authorIds, List<int> categoryIds);
        void Update(int id, string title, string? description, string? coverImage,
            List<int> authorIds, List<int> categoryIds);
        void Delete(int id);
        int CountAll();
        int CountEditions();
        int CountCopies();
        int CountAvailableCopies();
        List<BookBorrowStat> GetTopBorrowed(int top = 5);
    }
}
