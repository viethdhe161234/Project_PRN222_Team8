using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class UpdateAccountViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string NewUsername { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string NewEmail { get; set; }
    }
}
