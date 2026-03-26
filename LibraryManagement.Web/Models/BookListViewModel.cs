using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Web.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
