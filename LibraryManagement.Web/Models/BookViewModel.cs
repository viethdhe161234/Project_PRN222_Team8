using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required]
        public string? Description { get; set; }

        public string? CoverImage { get; set; }
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "At least one author is required")]
        public List<int> SelectedAuthorIds { get; set; } = new();

        [Required(ErrorMessage = "At least one category is required")]
        public List<int> SelectedCategoryIds { get; set; } = new();

        public List<SelectListItem> Authors { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
    }
}
