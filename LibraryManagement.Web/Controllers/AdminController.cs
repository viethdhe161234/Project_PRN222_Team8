using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;
        private readonly IAccountService _accountService;
        private readonly ILoanService _loanService;
        private readonly IBorrowRequestService _borrowRequestService;

        public AdminController(
            IBookService bookService,
            IMemberService memberService,
            IAccountService accountService,
            ILoanService loanService,
            IBorrowRequestService borrowRequestService)
        {
            _bookService = bookService;
            _memberService = memberService;
            _accountService = accountService;
            _loanService = loanService;
            _borrowRequestService = borrowRequestService;
        }

        public IActionResult Dashboard()
        {
            var allLoans = _loanService.GetActiveLoans().ToList();
            var overdueLoans = _loanService.GetOverdueLoans().ToList();
            var allRequests = _borrowRequestService.GetAll().ToList();
            var today = DateTime.Today;

            var model = new DashboardViewModel
            {               
                TotalBooks = _bookService.CountAll(),
                TotalEditions = _bookService.CountEditions(),
                TotalCopies = _bookService.CountCopies(),
                AvailableCopies = _bookService.CountAvailableCopies(),

                
                TotalMembers = _memberService.GetAll().Count(),
                ActiveAccounts = _accountService.GetAll().Count(a => a.IsActive),

              
                PendingRequests = allRequests.Count(r => r.Status == "Pending"),
                ActiveLoans = allLoans.Count,
                OverdueLoans = overdueLoans.Count,
                TopBorrowedBooks = _bookService.GetTopBorrowed(5),

               
                RecentRequests = allRequests.Take(5).ToList()
            };

            return View(model);
        }
    }
}