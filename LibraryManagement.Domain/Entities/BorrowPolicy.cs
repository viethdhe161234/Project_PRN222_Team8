using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BorrowPolicy
    {
        public int Id { get; set; }
        public int MaxBooksPerRequest { get; set; }
        public int MaxDaysBorrow { get; set; }
        public int MaxActiveLoan { get; set; }
    }
}
