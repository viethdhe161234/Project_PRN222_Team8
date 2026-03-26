using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Web.Models
{
    public class OverdueLoanViewModel
    {
        public IEnumerable<Loan> Loans { get; set; } = new List<Loan>();
        public int TotalOverdue { get; set; }
    }
}
