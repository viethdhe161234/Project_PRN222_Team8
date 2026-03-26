using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BorrowRequest
    {
        public int Id { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
        public ICollection<BorrowRequestItem> Items { get; set; }
    }
}
