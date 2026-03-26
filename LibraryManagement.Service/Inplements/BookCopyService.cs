using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _copyRepository;
        public BookCopyService(IBookCopyRepository copyRepository)
        {
            _copyRepository = copyRepository;
        }
        public void Create(int editionId, string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                throw new Exception("Barcode is required");
            if (_copyRepository.ExistsByBarcode(barcode))
                throw new Exception("Barcode already exists");

            _copyRepository.Add(new BookCopy
            {
                BookEditionId = editionId,
                Barcode = barcode,
                IsAvailable = true
            });
        }

        public void Delete(int id)
        {
            _copyRepository.Delete(id);
        }

        public IEnumerable<BookCopy> GetByEditionId(int editionId)
        {
            return _copyRepository.GetByEditionId(editionId);
        }
    }
}
