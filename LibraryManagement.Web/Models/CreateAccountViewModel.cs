using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
    }
}
