using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        public string Name { get; set; }
    }
}
