using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class BookCopyController : Controller
    {
        private readonly IBookCopyService _copyService;
        private readonly IBookEditionService _editionService;

        public BookCopyController(IBookCopyService copyService, IBookEditionService editionService)
        {
            _copyService = copyService;
            _editionService = editionService;
        }
        public IActionResult Index(int editionId)
        {
            var edition = _editionService.GetById(editionId);
            if (edition == null) return NotFound();

            ViewBag.Edition = edition;
            var copies = _copyService.GetByEditionId(editionId);
            return View(copies);
        }

        [HttpPost]
        public IActionResult Create(BookCopyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Barcode is required";
                return RedirectToAction("Index", new { editionId = model.EditionId });
            }

            try
            {
                _copyService.Create(model.EditionId, model.Barcode);
                TempData["Success"] = "Copy added successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", new { editionId = model.EditionId });
        }

        [HttpPost]
        public IActionResult Delete(int id, int editionId)
        {
            try
            {
                _copyService.Delete(id);
                TempData["Success"] = "Copy deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", new { editionId });
        }
    }
}
