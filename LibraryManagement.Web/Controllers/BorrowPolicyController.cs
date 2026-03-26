using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class BorrowPolicyController : Controller
    {
        private readonly IBorrowPolicyService _policyService;
        public BorrowPolicyController(IBorrowPolicyService policyService)
        {
            _policyService = policyService;
        }
        public IActionResult Index()
        {
            var policy = _policyService.Get();
            var model = new BorrowPolicyViewModel
            {
                MaxBooksPerRequest = policy.MaxBooksPerRequest,
                MaxDaysBorrow = policy.MaxDaysBorrow,
                MaxActiveLoan = policy.MaxActiveLoan
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(BorrowPolicyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _policyService.Update(
                    model.MaxBooksPerRequest,
                    model.MaxDaysBorrow,
                    model.MaxActiveLoan
                );
                TempData["Success"] = "Policy updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
