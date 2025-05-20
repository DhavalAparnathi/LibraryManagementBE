namespace Library.Models.Books
{
    public class BookList : PaginationModel
    {
        public string? Name { get; set; } = string.Empty;

        public string? Author { get; set; } = string.Empty;

        public int? GenreId { get; set; }
    }
}
