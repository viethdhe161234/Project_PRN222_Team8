using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("User")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult Index()
        {
            var memberId = Guid.Parse(HttpContext.Session.GetString("MemberId")!);
            var loans = _loanService.GetActiveByMemberId(memberId);
            return View(loans);
        }
        public IActionResult History()
        {
            var memberId = Guid.Parse(HttpContext.Session.GetString("MemberId")!);
            var loans = _loanService.GetAllByMemberId(memberId);
            return View(loans);
        }
    }
}
