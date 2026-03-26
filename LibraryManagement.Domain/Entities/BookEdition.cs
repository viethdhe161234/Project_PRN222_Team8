using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BookEdition
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PublishYear { get; set; }
        public string Publisher { get; set; }
        public ICollection<BookCopy> Copies { get; set; }
        public ICollection<BorrowRequestItem> BorrowRequestItems { get; set; }
    }
}
