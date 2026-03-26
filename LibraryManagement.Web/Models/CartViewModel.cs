namespace LibraryManagement.Web.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public int MaxBooksPerRequest { get; set; }
    }
}
