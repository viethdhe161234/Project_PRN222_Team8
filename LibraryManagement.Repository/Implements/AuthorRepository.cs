using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implements
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author? GetById(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            var existing = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existing == null) return;
            existing.Name = author.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return;

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
