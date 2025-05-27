namespace Library.Models.BorrowedBooks
{
    public class BorrowedBooks
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime? DateReturned { get; set; }

        public bool IsReturned { get; set; }
    }
}
