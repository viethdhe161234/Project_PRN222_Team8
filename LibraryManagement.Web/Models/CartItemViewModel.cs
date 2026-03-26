namespace LibraryManagement.Web.Models
{
    public class CartItemViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string? CoverImage { get; set; }
        public int EditionId { get; set; }
        public string Publisher { get; set; }
        public int PublishYear { get; set; }
    }
}
