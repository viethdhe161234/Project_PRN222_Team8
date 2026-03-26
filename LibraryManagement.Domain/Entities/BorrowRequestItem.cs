using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BorrowRequestItem
    {
        public int Id { get; set; }
        public int BorrowRequestId { get; set; }
        public BorrowRequest BorrowRequest { get; set; }
        public int BookEditionId { get; set; }
        public BookEdition BookEdition { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
