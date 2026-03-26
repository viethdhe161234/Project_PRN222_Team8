using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member? GetById(Guid id);
        void Update(Member member);
        int CountAll();
    }
}
