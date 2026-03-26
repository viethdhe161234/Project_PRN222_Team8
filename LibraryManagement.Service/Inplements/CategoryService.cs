using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("Category name is required");

            if (_categoryRepository.GetAll().Any(c => c.Name == category.Name))
                throw new Exception("Category already exists");

            _categoryRepository.Add(category);
        }

        public void Delete(int id)
        {
            var existing = _categoryRepository.GetById(id);
            if (existing == null)
                throw new Exception("Category not found");

            _categoryRepository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Update(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("Category name is required");

            var existing = _categoryRepository.GetById(category.Id);
            if (existing == null)
                throw new Exception("Category not found");

            _categoryRepository.Update(category);
        }
    }
}
