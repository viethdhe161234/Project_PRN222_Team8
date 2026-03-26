using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IBorrowRequestService
    {
        IEnumerable<BorrowRequest> GetAll();
        IEnumerable<BorrowRequest> GetByMemberId(Guid memberId);
        BorrowRequest? GetById(int id);
        void Create(Guid memberId, List<int> editionIds);
        void Accept(int requestId);
        void Reject(int requestId, string reason);
    }
}
