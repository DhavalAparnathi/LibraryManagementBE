using Library.Models.Books;

namespace Library.Business.ViewModel
{
    public class BookWithGenre : Books
    {
        public string GenreName { get; set; } = string.Empty;
    }

}
