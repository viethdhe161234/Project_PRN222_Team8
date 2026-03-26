using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Account? GetById(Guid id);
        Account? GetByUsername(string username);
        Account? GetByMemberId(Guid memberId);
        Account? GetByEmail(string email);
        void Add(Account account);
        void Update(Account account);
        IEnumerable<Account> GetAll();
        void Delete(Guid id);
        int CountActive();
    }
}
