using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly ICloudinaryService _cloudinaryService;

        public BookController(IBookService bookService,
            IAuthorService authorService, ICategoryService categoryService, ICloudinaryService cloudinaryService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            return View(books);
        }

        public IActionResult Detail(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            return View(BuildViewModel());
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            TempData["Debug"] = $"Authors={string.Join(",", model.SelectedAuthorIds)}, Categories={string.Join(",", model.SelectedCategoryIds)}";
            if (!ModelState.IsValid)
            {
                BuildViewModel(model);
                return View(model);
            }
            try
            {
                var coverImage = HandleImage(model.ImageFile, model.CoverImage);
                _bookService.Create(model.Title, model.Description, model.CoverImage,
                    model.SelectedAuthorIds, model.SelectedCategoryIds);
                TempData["Success"] = "Book created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                BuildViewModel(model);
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();

            var model = BuildViewModel();
            model.Id = book.Id;
            model.Title = book.Title;
            model.Description = book.Description;
            model.CoverImage = book.CoverImage;
            model.SelectedAuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList();
            model.SelectedCategoryIds = book.BookCategories.Select(bc => bc.CategoryId).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BookViewModel model)
        {
            var coverI = HandleImage(model.ImageFile, model.CoverImage);
            TempData["Debug"] = $"CoverImage={coverI ?? "null"}";
            if (!ModelState.IsValid)
            {
                BuildViewModel(model);
                return View(model);
            }

            try
            {
                var coverImage = HandleImage(model.ImageFile, model.CoverImage);
                _bookService.Update(model.Id, model.Title, model.Description, coverImage,
                    model.SelectedAuthorIds, model.SelectedCategoryIds);
                TempData["Success"] = "Book updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                BuildViewModel(model);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _bookService.Delete(id);
                TempData["Success"] = "Book deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        private string? HandleImage(IFormFile? imageFile, string? coverImageUrl)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using var stream = imageFile.OpenReadStream();
                return _cloudinaryService.UploadImage(
                    stream, imageFile.FileName, "library/books"
                );
            }
            return coverImageUrl;
        }
        private BookViewModel BuildViewModel(BookViewModel? model = null)
        {
            var authors = _authorService.GetAll()
                .Select(a => new SelectListItem(a.Name, a.Id.ToString()))
                .ToList();

            var categories = _categoryService.GetAll()
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();

            if (model == null)
                return new BookViewModel { Authors = authors, Categories = categories };

            model.Authors = authors;
            model.Categories = categories;
            return model;
        }
    }
}
