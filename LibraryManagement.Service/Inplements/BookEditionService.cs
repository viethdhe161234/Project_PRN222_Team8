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
    public class BookEditionService : IBookEditionService
    {
        private readonly IBookEditionRepository _editionRepository;

        public BookEditionService(IBookEditionRepository editionRepository)
        {
            _editionRepository = editionRepository;
        }
        public void Create(int bookId, string publisher, int publishYear)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                throw new Exception("Publisher is required");

            if (publishYear < 1000 || publishYear > DateTime.Now.Year)
                throw new Exception("Invalid publish year");

            _editionRepository.Add(new BookEdition
            {
                BookId = bookId,
                Publisher = publisher,
                PublishYear = publishYear
            });
        }

        public void Delete(int id)
        {
            _editionRepository.Delete(id);
        }

        public int GetBookIdByEditionId(int editionId)
        {
            return _editionRepository.GetBookIdByEditionId(editionId);
        }

        public BookEdition? GetById(int id)
        {
            return _editionRepository.GetById(id);
        }

        public void Update(int id, string publisher, int publishYear)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                throw new Exception("Publisher is required");

            if (publishYear < 1000 || publishYear > DateTime.Now.Year)
                throw new Exception("Invalid publish year");

            _editionRepository.Update(new BookEdition
            {
                Id = id,
                Publisher = publisher,
                PublishYear = publishYear
            });
        }
    }
}
