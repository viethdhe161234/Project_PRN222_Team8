using LibraryManagement.Domain.Entities;
using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            var authors = _authorService.GetAll();
            return View(authors);
        }

        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(AuthorViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            try
            {
                _authorService.Create(new Author { Name = model.Name });
                TempData["Success"] = "Author created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public IActionResult Edit(int id)
        {
            var author = _authorService.GetById(id);
            if (author == null) 
                return NotFound();

            return View(new AuthorViewModel { Id = author.Id, Name = author.Name });
        }
        [HttpPost]
        public IActionResult Edit(AuthorViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            try
            {
                _authorService.Update(new Author { Id = model.Id, Name = model.Name });
                TempData["Success"] = "Author updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _authorService.Delete(id);
                TempData["Success"] = "Author deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
