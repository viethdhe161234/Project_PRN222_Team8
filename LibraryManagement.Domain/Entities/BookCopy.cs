using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookEditionId { get; set; }
        public BookEdition BookEdition { get; set; }
        public string Barcode { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
