using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.IssuedBooks
{
    public class IssuedBooks
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime? DateReturned { get; set; }

        public bool IsReturned { get; set; }
    }
}
