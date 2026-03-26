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
    public class AccountRepository : IAccountRepository
    {
        private readonly LibraryDbContext _context;
        public AccountRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public int CountActive()
        {
            return _context.Accounts.Count(a => a.IsActive);
        }

        public void Delete(Guid id)
        {
            var account = _context.Accounts
                .Include(a => a.Member)
                .FirstOrDefault(a => a.Id == id);

            if (account != null)
            {
                if (account.Member != null)
                    _context.Members.Remove(account.Member);

                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Account> GetAll()
        {
            return  _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Member)
            .ToList();
        }

        public Account? GetByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public Account? GetById(Guid id)
        {
            return _context.Accounts.Include(a => a.Role).Include(a => a.Member).FirstOrDefault(a => a.Id == id);
        }

        public Account? GetByMemberId(Guid memberId)
        {
            return _context.Accounts
            .Include(a => a.Role)
            .FirstOrDefault(a => a.MemberId == memberId);
        }

        public Account? GetByUsername(string username)
        {
            return _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Member)
            .FirstOrDefault(a => a.Username == username);
        }

        public void Update(Account account)
        {
            var existing = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if (existing == null) return;

            existing.Username = account.Username;
            existing.Email = account.Email;
            existing.RoleId = account.RoleId;
            existing.IsActive = account.IsActive;
            existing.PasswordHash = account.PasswordHash;

            _context.SaveChanges();
        }
    }
}
