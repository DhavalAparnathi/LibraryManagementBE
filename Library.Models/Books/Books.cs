using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Books
{
    public class Books
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public DateTime AddedAt { get; set; }

        public int AddedBy { get; set; }  // To map for the User who added it
    }
}
