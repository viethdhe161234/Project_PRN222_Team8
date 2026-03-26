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
    public class BorrowPolicyService : IBorrowPolicyService
    {
        private readonly IBorrowPolicyRepository _policyRepository;
        public BorrowPolicyService(IBorrowPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }
        public BorrowPolicy Get()
        {
            return _policyRepository.Get();
        }

        public void Update(int maxBooksPerRequest, int maxDaysBorrow, int maxActiveLoan)
        {
            if (maxBooksPerRequest < 1)
                throw new Exception("Max books per request must be at least 1");

            if (maxDaysBorrow < 1)
                throw new Exception("Max days borrow must be at least 1");

            if (maxActiveLoan < 1)
                throw new Exception("Max active loan must be at least 1");

            if (maxBooksPerRequest > maxActiveLoan)
                throw new Exception("Max books per request cannot exceed max active loans");

            _policyRepository.Update(new BorrowPolicy
            {
                MaxBooksPerRequest = maxBooksPerRequest,
                MaxDaysBorrow = maxDaysBorrow,
                MaxActiveLoan = maxActiveLoan
            });
        }
    }
}
