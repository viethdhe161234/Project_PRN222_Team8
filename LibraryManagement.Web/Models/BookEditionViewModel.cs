using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class BookEditionViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Publish year is required")]
        [Range(1000, 2100, ErrorMessage = "Invalid publish year")]
        public int PublishYear { get; set; }
    }
}
