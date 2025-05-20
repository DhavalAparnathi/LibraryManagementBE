using Library.Models.Books;
using Library.Business.ViewModel;

namespace Library.Services.Book
{
    public interface IBookService
    {
        /// <summary>
        /// Retrieves a paginated list of books from the database based on filter and sort criteria.
        /// </summary>
        (List<BookWithGenre> Books, int TotalCount) GetBookList(BookList model, int userId);

        /// <summary>
        /// Deletes a book record from the database based on the book ID.
        /// </summary>
        void DeleteBookById(int bookId);

        /// <summary>
        /// Inserts or updates a book record in the database.
        /// </summary>
        void UpsertBook(BooksUpsertViewModel model, int addedBy);

        /// <summary>
        /// Gets the list of all the Genres 
        /// </summary>
        List<GenreViewModel> GetAllGenres();

    }
}
