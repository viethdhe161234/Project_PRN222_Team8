using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
    }
}
