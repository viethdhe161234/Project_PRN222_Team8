using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        void Create(Author author);
        void Update(Author author);
        void Delete(int id);
    }
}
