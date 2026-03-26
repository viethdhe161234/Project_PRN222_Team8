using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class BookEditionController : Controller
    {
        private readonly IBookEditionService _editionService;
        public BookEditionController(IBookEditionService editionService)
        {
            _editionService = editionService;
        }
        public IActionResult Create(int bookId)
        {
            return View(new BookEditionViewModel { BookId = bookId });
        }

        [HttpPost]
        public IActionResult Create(BookEditionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _editionService.Create(model.BookId, model.Publisher, model.PublishYear);
                TempData["Success"] = "Edition added successfully";
                return RedirectToAction("Detail", "Book", new { id = model.BookId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var edition = _editionService.GetById(id);
            if (edition == null) return NotFound();

            return View(new BookEditionViewModel
            {
                Id = edition.Id,
                BookId = edition.BookId,
                Publisher = edition.Publisher,
                PublishYear = edition.PublishYear
            });
        }

        [HttpPost]
        public IActionResult Edit(BookEditionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _editionService.Update(model.Id, model.Publisher, model.PublishYear);
                TempData["Success"] = "Edition updated successfully";
                return RedirectToAction("Detail", "Book", new { id = model.BookId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id, int bookId)
        {
            try
            {
                _editionService.Delete(id);
                TempData["Success"] = "Edition deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Detail", "Book", new { id = bookId });
        }
    }
}
