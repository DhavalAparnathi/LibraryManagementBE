namespace Library.Business.ViewModel
{
    public class BooksUpsertViewModel
    {
        public int Id { get; set; } 

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int GenreId { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

    }
}
