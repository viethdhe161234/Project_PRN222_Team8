using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IBorrowPolicyService
    {
        BorrowPolicy Get();
        void Update(int maxBooksPerRequest, int maxDaysBorrow, int maxActiveLoan);
    }
}
