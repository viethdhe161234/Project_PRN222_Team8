using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IEnumerable<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member? GetById(Guid id)
        {
            return _memberRepository.GetById(id);
        }

        public bool IsUserRole(Guid memberId)
        {
            var member = _memberRepository.GetById(memberId);
            return member?.Account?.Role.Name == "User";
        }

        public void Update(Guid id, string fullName, string? phone, string? address)
        {
            var member = _memberRepository.GetById(id);
            if (member == null)
                throw new Exception("Member not found");

            member.FullName = fullName;
            member.Phone = phone;
            member.Address = address;

            _memberRepository.Update(member);
        }
    }
}
