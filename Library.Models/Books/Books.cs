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

        public int AddedBy { get; set; }

        public bool HasIssued { get; set; }

        public int? IssueId { get; set; }

        public int? IssuedUserCount { get; set; }



    }
}
