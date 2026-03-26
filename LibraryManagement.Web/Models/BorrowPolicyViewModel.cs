using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class BorrowPolicyViewModel
    {
        [Required]
        [Range(1, 20, ErrorMessage = "Must be between 1 and 20")]
        public int MaxBooksPerRequest { get; set; }

        [Required]
        [Range(1, 365, ErrorMessage = "Must be between 1 and 365")]
        public int MaxDaysBorrow { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Must be between 1 and 20")]
        public int MaxActiveLoan { get; set; }
    }
}
