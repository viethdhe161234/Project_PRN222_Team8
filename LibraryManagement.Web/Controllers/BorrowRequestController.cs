using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("User")]
    public class BorrowRequestController : Controller
    {
        private readonly IBorrowRequestService _borrowRequestService;
        private const string CartKey = "Cart";

        public BorrowRequestController(IBorrowRequestService borrowRequestService)
        {
            _borrowRequestService = borrowRequestService;
        }
        public IActionResult Index()
        {
            var memberId = GetMemberId();
            var requests = _borrowRequestService.GetByMemberId(memberId);
            return View(requests);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var memberId = GetMemberId();
            var cart = GetCart();

            try
            {
                _borrowRequestService.Create(memberId, cart);
                HttpContext.Session.Remove(CartKey);
                TempData["Success"] = "Borrow request submitted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        
        private Guid GetMemberId()
        {
            var memberId = HttpContext.Session.GetString("MemberId");
            return Guid.Parse(memberId!);
        }

        private List<int> GetCart()
        {
            var json = HttpContext.Session.GetString(CartKey);
            return string.IsNullOrEmpty(json)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(json)!;
        }
    }
}
