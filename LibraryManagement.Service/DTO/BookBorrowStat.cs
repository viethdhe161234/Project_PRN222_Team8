using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.DTO
{
    public class BookBorrowStat
    {
        public string Title { get; set; } = string.Empty;
        public string? CoverImage { get; set; }
        public int BorrowCount { get; set; }
    }
}
