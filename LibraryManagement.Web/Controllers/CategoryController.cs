using LibraryManagement.Domain.Entities;
using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _categoryService.Create(new Category { Name = model.Name });
                TempData["Success"] = "Category created successfully";
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
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            return View(new CategoryViewModel { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _categoryService.Update(new Category { Id = model.Id, Name = model.Name });
                TempData["Success"] = "Category updated successfully";
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
                _categoryService.Delete(id);
                TempData["Success"] = "Category deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }

}
