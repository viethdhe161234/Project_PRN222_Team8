using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public Account Account { get; set; }
        public ICollection<BorrowRequest> BorrowRequests { get; set; }
        public ICollection<Loan> Loans { get; set; }

    }
}
