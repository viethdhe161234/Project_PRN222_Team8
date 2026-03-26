using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implements
{
    public class BookEditionRepository : IBookEditionRepository
    {
        private readonly LibraryDbContext _context;
        public BookEditionRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(BookEdition edition)
        {
            _context.BookEditions.Add(edition);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var edition = _context.BookEditions.FirstOrDefault(e => e.Id == id);
            if (edition == null) return;

            _context.BookEditions.Remove(edition);
            _context.SaveChanges();
        }

        public int GetBookIdByEditionId(int editionId)
        {
            return _context.BookEditions
                .Where(e => e.Id == editionId)
                .Select(e => e.BookId)
                .FirstOrDefault();
        }

        public BookEdition? GetById(int id)
        {
            return _context.BookEditions
                .Include(e => e.Copies)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Update(BookEdition edition)
        {
            var existing = _context.BookEditions.FirstOrDefault(e => e.Id == edition.Id);
            if (existing == null) return;

            existing.Publisher = edition.Publisher;
            existing.PublishYear = edition.PublishYear;

            _context.SaveChanges();
        }
    }
}
