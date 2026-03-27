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
    public class BorrowRequestRepository : IBorrowRequestRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRequestRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BorrowRequest> GetAll()
        {
            return _context.BorrowRequests
                .Include(r => r.Member)
                .Include(r => r.Items)
                .ThenInclude(i => i.BookEdition)
                .ThenInclude(e => e.Book)
                .OrderByDescending(r => r.RequestDate)
                .ToList();
        }

        public IEnumerable<BorrowRequest> GetByMemberId(Guid memberId)
        {
            return _context.BorrowRequests
                .Include(r => r.Items)
                .ThenInclude(i => i.BookEdition)
                .ThenInclude(e => e.Book)
                .Where(r => r.MemberId == memberId)
                .OrderByDescending(r => r.RequestDate)
                .ToList();
        }

        public BorrowRequest? GetById(int id)
        {
            return _context.BorrowRequests
                .Include(r => r.Member)
                .Include(r => r.Items)
                .ThenInclude(i => i.BookEdition)
                .ThenInclude(e => e.Book)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(BorrowRequest request)
        {
            _context.BorrowRequests.Add(request);
            _context.SaveChanges();
        }

        public void Update(BorrowRequest request)
        {
            var existing = _context.BorrowRequests.FirstOrDefault(r => r.Id == request.Id);
            if (existing == null) return;

            existing.Status = request.Status;
            existing.RejectionReason = request.RejectionReason;
            _context.SaveChanges();
        }

        public bool HasActiveRequestForBook(Guid memberId, int bookId)
        {
            return _context.BorrowRequests
                .Where(r => r.MemberId == memberId && (r.Status == "Pending"))
                .Any(r => r.Items.Any(i => i.BookEdition.BookId == bookId));
        }
    }
}
