using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IBorrowRequestRepository
    {
        IEnumerable<BorrowRequest> GetAll();
        IEnumerable<BorrowRequest> GetByMemberId(Guid memberId);
        BorrowRequest? GetById(int id);
        void Add(BorrowRequest request);
        void Update(BorrowRequest request);
        bool HasActiveRequestForBook(Guid memberId, int bookId);
    }
}
