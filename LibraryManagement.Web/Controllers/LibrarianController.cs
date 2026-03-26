using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Librarian")]
    public class LibrarianController : Controller
    {
        private readonly IBorrowRequestService _borrowRequestService;
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;

        public LibrarianController(
            IBorrowRequestService borrowRequestService,
            ILoanService loanService,
            IBookService bookService)
        {
            _borrowRequestService = borrowRequestService;
            _loanService = loanService;
            _bookService = bookService;
        }

        public IActionResult Dashboard()
        {
            var allRequests = _borrowRequestService.GetAll().ToList();
            var overdueLoans = _loanService.GetOverdueLoans().ToList();
            var dueSoon = _loanService.GetDueSoon(3).ToList();

            var model = new DashboardViewModel
            {
                PendingRequests = allRequests.Count(r => r.Status == "Pending"),
                ActiveLoans = _loanService.GetActiveLoans().Count(),
                OverdueLoans = overdueLoans.Count,
                AvailableCopies = _bookService.CountAvailableCopies(),
                TotalCopies = _bookService.CountCopies(),
                TotalBooks = _bookService.CountAll(),

                OverdueLoansList = overdueLoans,
                DueSoonLoans = dueSoon,

                RecentRequests = allRequests
                    .Where(r => r.Status == "Pending")
                    .OrderBy(r => r.RequestDate)  
                    .ToList()
            };

            ViewBag.OverdueCount = overdueLoans.Count;
            return View(model);
        }

        public IActionResult PendingRequests()
        {
            ViewBag.OverdueCount = _loanService.GetOverdueLoans().Count();
            return View(_borrowRequestService.GetAll()
                .Where(r => r.Status == "Pending").ToList());
        }

        public IActionResult ActiveLoans()
        {
            ViewBag.OverdueCount = _loanService.GetOverdueLoans().Count();
            return View(_loanService.GetActiveLoans());
        }

        public IActionResult OverdueLoans()
        {
            var loans = _loanService.GetOverdueLoans().ToList();
            ViewBag.OverdueCount = loans.Count;
            return View(new OverdueLoanViewModel
            {
                Loans = loans,
                TotalOverdue = loans.Count
            });
        }

        [HttpPost]
        public IActionResult Accept(int id)
        {
            try
            {
                _borrowRequestService.Accept(id);
                TempData["Success"] = "Request accepted successfully";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Reject(int id, string reason)
        {
            try
            {
                _borrowRequestService.Reject(id, reason);
                TempData["Success"] = "Request rejected";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult ConfirmReturn(int loanId)
        {
            try
            {
                _loanService.ConfirmReturn(loanId);
                TempData["Success"] = "Return confirmed successfully";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction("ActiveLoans");
        }
    }
}