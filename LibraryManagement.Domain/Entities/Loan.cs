using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BorrowRequestItemId { get; set; }
        public BorrowRequestItem BorrowRequestItem { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue => ReturnDate == null && DateTime.Now > DueDate;
        public int OverdueDays => IsOverdue ? (int)(DateTime.Now - DueDate).TotalDays : 0;
    }
}
