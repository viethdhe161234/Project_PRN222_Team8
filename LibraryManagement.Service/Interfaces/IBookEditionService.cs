using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IBookEditionService
    {
        BookEdition? GetById(int id);
        void Create(int bookId, string publisher, int publishYear);
        void Update(int id, string publisher, int publishYear);
        void Delete(int id);
        int GetBookIdByEditionId(int editionId);
    }
}
