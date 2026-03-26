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
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        public LoanService(ILoanRepository loanRepository,
            IBookCopyRepository bookCopyRepository)
        {
            _loanRepository = loanRepository;
            _bookCopyRepository = bookCopyRepository;
        }
        public void ConfirmReturn(int loanId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
                throw new Exception("Loan not found");

            if (loan.ReturnDate != null)
                throw new Exception("Loan already returned");

            loan.ReturnDate = DateTime.Now;
            _loanRepository.Update(loan);

            var copy = _bookCopyRepository.GetById(loan.BookCopyId);
            if (copy != null)
            {
                copy.IsAvailable = true;
                _bookCopyRepository.Update(copy);
            }
        }

        public IEnumerable<Loan> GetActiveByMemberId(Guid memberId)
        {
            return _loanRepository.GetActiveByMemberId(memberId);
        }

        public IEnumerable<Loan> GetActiveLoans()
        {
            return _loanRepository.GetActiveLoans();
        }

        public IEnumerable<Loan> GetAllByMemberId(Guid memberId)
        {
            return _loanRepository.GetAllByMemberId(memberId);
        }

        public IEnumerable<Loan> GetDueSoon(int withinDays = 3)
        {
            return _loanRepository.GetDueSoon(withinDays);
        }

        public IEnumerable<Loan> GetOverdueLoans()
        {
            return _loanRepository.GetOverdueLoans();
        }
    }
}
