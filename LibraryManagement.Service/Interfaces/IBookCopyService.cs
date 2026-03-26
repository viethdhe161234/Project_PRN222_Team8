using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IBookCopyService
    {
        IEnumerable<BookCopy> GetByEditionId(int editionId);
        void Create(int editionId, string barcode);
        void Delete(int id);
    }
}
