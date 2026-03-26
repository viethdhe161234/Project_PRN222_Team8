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
    public class BorrowPolicyRepository : IBorrowPolicyRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowPolicyRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public BorrowPolicy Get()
        {
            return _context.BorrowPolicies.First();
        }

        public void Update(BorrowPolicy policy)
        {
            var existing = _context.BorrowPolicies.First();

            existing.MaxBooksPerRequest = policy.MaxBooksPerRequest;
            existing.MaxDaysBorrow = policy.MaxDaysBorrow;
            existing.MaxActiveLoan = policy.MaxActiveLoan;

            _context.SaveChanges();
        }
    }
}
