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
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public int CountActive()
        {
            return _context.Loans.Count(l => l.ReturnDate == null);
        }

        public int CountActiveByMemberId(Guid memberId)
        {
            return _context.Loans
                .Count(l => l.MemberId == memberId && l.ReturnDate == null);
        }

        public int CountOverdue()
        {
            var now = DateTime.Now;
            return _context.Loans.Count(l => l.ReturnDate == null && l.DueDate < now);
        }

        public int CountReturnedToday()
        {
            var today = DateTime.Today;
            return _context.Loans.Count(l => l.ReturnDate != null && l.ReturnDate.Value.Date == today);
        }

        public IEnumerable<Loan> GetActiveByMemberId(Guid memberId)
        {
            return _context.Loans
                .Include(l => l.BookCopy)
                .ThenInclude(c => c.BookEdition)
                .ThenInclude(e => e.Book)
                .Where(l => l.MemberId == memberId && l.ReturnDate == null)
                .ToList();
        }

        public IEnumerable<Loan> GetActiveLoans()
        {
            return _context.Loans
                .Include(l => l.Member)
                .Include(l => l.BookCopy)
                .ThenInclude(c => c.BookEdition)
                .ThenInclude(e => e.Book)
                .Where(l => l.ReturnDate == null)
                .OrderBy(l => l.DueDate)
                .ToList();
        }

        public IEnumerable<Loan> GetAllByMemberId(Guid memberId)
        {
            return _context.Loans
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.BookEdition)
                    .ThenInclude(e => e.Book)
                    .Where(l => l.MemberId == memberId)
                    .OrderByDescending(l => l.BorrowDate)
                    .ToList();
        }

        public Loan? GetById(int id)
        {
            return _context.Loans
                .Include(l => l.BookCopy)
                .ThenInclude(c => c.BookEdition)
                .ThenInclude(e => e.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Loan> GetDueSoon(int withinDays = 3)
        {
            var now = DateTime.Now;
            var cutoff = now.AddDays(withinDays);
            return _context.Loans
                .Include(l => l.Member)
                .Include(l => l.BookCopy).ThenInclude(c => c.BookEdition).ThenInclude(e => e.Book)
                .Where(l => l.ReturnDate == null && l.DueDate >= now && l.DueDate <= cutoff)
                .OrderBy(l => l.DueDate).ToList();
        }

        public IEnumerable<Loan> GetOverdueLoans()
        {
            var now = DateTime.Now;
            return _context.Loans
                .Include(l => l.Member)
                    .ThenInclude(m => m.Account)
                    .Include(l => l.BookCopy)
                    .ThenInclude(c => c.BookEdition)
                    .ThenInclude(e => e.Book)
                    .Where(l => l.ReturnDate == null && l.DueDate < now)
                    .OrderBy(l => l.DueDate)
                    .ToList();
        }

        public void Update(Loan loan)
        {
            var existing = _context.Loans.FirstOrDefault(l => l.Id == loan.Id);
            if (existing == null) return;

            existing.ReturnDate = loan.ReturnDate;
            _context.SaveChanges();
        }
    }
}
