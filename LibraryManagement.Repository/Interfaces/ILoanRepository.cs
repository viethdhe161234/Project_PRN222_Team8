using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetActiveLoans();
        IEnumerable<Loan> GetActiveByMemberId(Guid memberId);
        IEnumerable<Loan> GetDueSoon(int withinDays = 3);
        IEnumerable<Loan> GetOverdueLoans();                   
        IEnumerable<Loan> GetAllByMemberId(Guid memberId);
        int CountActiveByMemberId(Guid memberId);
        Loan? GetById(int id);
        void Add(Loan loan);
        void Update(Loan loan);
        int CountActive();
        int CountOverdue();
        int CountReturnedToday();
    }
}
