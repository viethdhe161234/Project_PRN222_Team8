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
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public int CountAll()
        {
            return _context.Members.Count();
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public Member? GetById(Guid id)
        {
            return _context.Members.Include(m => m.Account)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Update(Member member)
        {
            var existing = _context.Members.FirstOrDefault(m => m.Id == member.Id);
            if (existing == null) return;

            existing.FullName = member.FullName;
            existing.Phone = member.Phone;
            existing.Address = member.Address;

            _context.SaveChanges();
        }
    }
}
