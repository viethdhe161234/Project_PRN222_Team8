using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class BookCopyViewModel
    {
        public int EditionId { get; set; }

        [Required(ErrorMessage = "Barcode is required")]
        public string Barcode { get; set; }
    }
}
