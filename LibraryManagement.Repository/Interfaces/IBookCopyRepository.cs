using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IBookCopyRepository
    {
        IEnumerable<BookCopy> GetByEditionId(int editionId);
        BookCopy? GetById(int id);
        BookCopy? GetAvailableByEditionId(int editionId);
        bool ExistsByBarcode(string barcode);
        void Add(BookCopy copy);
        void Update(BookCopy copy);
        void Delete(int id);
    }
}
