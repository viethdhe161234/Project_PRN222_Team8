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
    public class BorrowRequestService : IBorrowRequestService
    {
        private readonly IBorrowRequestRepository _requestRepository;
        private readonly IBorrowPolicyRepository _policyRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IBookEditionRepository _bookEditionRepository;


        public BorrowRequestService(IBorrowRequestRepository requestRepository,
            IBorrowPolicyRepository policyRepository,
            ILoanRepository loanRepository,
            IBookCopyRepository bookCopyRepository,
            IBookEditionRepository bookEditionRepository)
        {
            _requestRepository = requestRepository;
            _policyRepository = policyRepository;
            _loanRepository = loanRepository;
            _bookCopyRepository = bookCopyRepository;
            _bookEditionRepository = bookEditionRepository;
        }

        public void Accept(int requestId)
        {
            var request = _requestRepository.GetById(requestId);
            if (request == null)
                throw new Exception("Request not found");

            if (request.Status != "Pending")
                throw new Exception("Request is no longer pending");

            var policy = _policyRepository.Get();

            foreach (var item in request.Items)
            {
                
                var copy = _bookCopyRepository.GetAvailableByEditionId(item.BookEditionId);
                if (copy == null)
                    throw new Exception($"No available copy for edition {item.BookEditionId}");

                _loanRepository.Add(new Loan
                {
                    BorrowRequestItemId = item.Id,
                    MemberId = request.MemberId,
                    BookCopyId = copy.Id,
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(policy.MaxDaysBorrow),
                    ReturnDate = null
                });
               
                copy.IsAvailable = false;
                _bookCopyRepository.Update(copy);
            }
            request.Status = "Approved";
            _requestRepository.Update(request);
        }

        public void Create(Guid memberId, List<int> editionIds)
        {
            if (!editionIds.Any())
                throw new Exception("Cart is empty");

            var policy = _policyRepository.Get();

            
            if (editionIds.Count > policy.MaxBooksPerRequest)
                throw new Exception($"Maximum {policy.MaxBooksPerRequest} books per request");

            
            var activeLoans = _loanRepository.CountActiveByMemberId(memberId);
            if (activeLoans + editionIds.Count > policy.MaxActiveLoan)
                throw new Exception($"You would exceed the maximum of {policy.MaxActiveLoan} active loans");
            foreach (var editionId in editionIds)
            {
                var bookId = _bookEditionRepository.GetBookIdByEditionId(editionId);
                if (_requestRepository.HasActiveRequestForBook(memberId, bookId))
                    throw new Exception("You already have an active request for one of the selected books");
            }

            var request = new BorrowRequest
            {
                MemberId = memberId,
                RequestDate = DateTime.Now,
                Status = "Pending",
                Items = editionIds.Select(id => new BorrowRequestItem
                {
                    BookEditionId = id
                }).ToList()
            };

            _requestRepository.Add(request);
        }

        public IEnumerable<BorrowRequest> GetAll()
        {
            return _requestRepository.GetAll();
        }

        public BorrowRequest? GetById(int id)
        {
            return _requestRepository.GetById(id);
        }

        public IEnumerable<BorrowRequest> GetByMemberId(Guid memberId)
        {
            return _requestRepository.GetByMemberId(memberId);
        }

        public void Reject(int requestId, string reason)
        {
            var request = _requestRepository.GetById(requestId);
            if (request == null)
                throw new Exception("Request not found");

            if (request.Status != "Pending")
                throw new Exception("Request is no longer pending");

            request.Status = "Rejected";
            request.RejectionReason = reason;
            _requestRepository.Update(request);
        }
    }
}
