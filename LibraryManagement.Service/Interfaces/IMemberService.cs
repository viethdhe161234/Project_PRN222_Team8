using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAll();
        Member? GetById(Guid id);
        void Update(Guid id, string fullName, string? phone, string? address);
        bool IsUserRole(Guid memberId);
    }
}
