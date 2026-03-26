using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class EditAccountViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
    }
}
