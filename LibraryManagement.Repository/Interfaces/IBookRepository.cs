using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);

        int CountAll();
        int CountEditions();
        int CountCopies();
        int CountAvailableCopies();
        List<(string Title, string? CoverImage, int BorrowCount)> GetTopBorrowed(int top = 5);
    }
}
