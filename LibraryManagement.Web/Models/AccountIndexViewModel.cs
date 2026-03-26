using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Web.Models
{
    public class AccountIndexViewModel
    {
        public string? SearchTerm { get; set; }
        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
    }
}
