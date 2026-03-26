using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class EditMemberViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
