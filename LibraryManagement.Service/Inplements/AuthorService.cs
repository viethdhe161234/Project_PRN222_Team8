using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public void Create(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new Exception("Author name is required");

            if (_authorRepository.GetAll().Any(a => a.Name == author.Name))
                throw new Exception("Author already exists");
            _authorRepository.Add(author);
        }

        public void Delete(int id)
        {
            var existing = _authorRepository.GetById(id);
            if (existing == null)
                throw new Exception("Author not found");

            try
            {
                _authorRepository.Delete(id);
            }
            catch (Exception)
            {
                throw new Exception("Cannot delete author because it is linked to one or more books.");
            }
        }

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public void Update(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new Exception("Author name is required");

            _authorRepository.Update(author);
        }
    }
}
