using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }

        [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
