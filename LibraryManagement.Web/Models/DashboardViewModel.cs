using LibraryManagement.Domain.Entities;
using LibraryManagement.Service.DTO;

namespace LibraryManagement.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalEditions { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public int TotalMembers { get; set; }
        public int ActiveAccounts { get; set; }
        public int PendingRequests { get; set; }
        public int ActiveLoans { get; set; }
        public int OverdueLoans { get; set; }

        public List<Loan> OverdueLoansList { get; set; } = new();
        public List<Loan> DueSoonLoans { get; set; } = new();

        public List<BookBorrowStat> TopBorrowedBooks { get; set; } = new();
        public List<BorrowRequest> RecentRequests { get; set; } = new();

    }

}
