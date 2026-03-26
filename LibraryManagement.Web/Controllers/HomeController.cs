using System.Diagnostics;
using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public HomeController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAll().Take(8).ToList();
            return View(books);
        }

        public IActionResult Books(string? searchTerm, int? categoryId)
        {
            var books = _bookService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                books = books.Where(b =>
                    b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.BookAuthors.Any(ba => ba.Author.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                );

            if (categoryId.HasValue)
                books = books.Where(b =>
                    b.BookCategories.Any(bc => bc.CategoryId == categoryId)
                );

            var model = new BookListViewModel
            {
                Books = books,
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        public IActionResult BookDetail(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }
    
    }
}
