using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetActiveLoans();
        IEnumerable<Loan> GetActiveByMemberId(Guid memberId);
        IEnumerable<Loan> GetOverdueLoans(); 
        IEnumerable<Loan> GetAllByMemberId(Guid memberId);
        IEnumerable<Loan> GetDueSoon(int withinDays = 3);
        void ConfirmReturn(int loanId);
    }
}
