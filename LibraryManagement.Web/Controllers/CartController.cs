using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("User")]
    public class CartController : Controller
    {

        private readonly IBookService _bookService;
        private readonly IBorrowPolicyService _policyService;
        private readonly ICartService _cartService;
        private const string CartKey = "Cart";

        public CartController(IBookService bookService, ICartService cartService,
            IBorrowPolicyService policyService)
        {
            _bookService = bookService;
            _cartService = cartService;
            _policyService = policyService;
        }

        public IActionResult Index()
        {
            var editionIds = GetCart();
            var books = _bookService.GetAll();

            var cartItems = books
                .SelectMany(b => b.Editions
                    .Where(e => editionIds.Contains(e.Id))
                    .Select(e => new CartItemViewModel
                    {
                        BookId = b.Id,
                        BookTitle = b.Title,
                        CoverImage = b.CoverImage,
                        EditionId = e.Id,
                        Publisher = e.Publisher,
                        PublishYear = e.PublishYear
                    }))
                .ToList();

            var policy = _policyService.Get();
            var model = new CartViewModel
            {
                Items = cartItems,
                MaxBooksPerRequest = policy.MaxBooksPerRequest
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(int editionId)
        {
            var cart = GetCart();

            try
            {
                _cartService.AddToCart(editionId, cart);
                SaveCart(cart);
                TempData["Success"] = "Added to cart";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Remove(int editionId)
        {
            var cart = GetCart();
            _cartService.RemoveFromCart(editionId, cart);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartKey);
            return RedirectToAction("Index");
        }

        private List<int> GetCart()
        {
            var json = HttpContext.Session.GetString(CartKey);
            return string.IsNullOrEmpty(json)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(json)!;
        }

        private void SaveCart(List<int> cart)
        {
            HttpContext.Session.SetString(CartKey, JsonSerializer.Serialize(cart));
        }
    }
}
