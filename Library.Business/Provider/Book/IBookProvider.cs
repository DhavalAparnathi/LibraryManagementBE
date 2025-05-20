using Library.Business.ViewModel;
using Library.Models.Books;

namespace Library.Business.Provider.Book
{
    public interface IBookProvider
    {
        /// <summary>
        /// Retrieves a paginated list of books with optional filtering by name and author.
        /// </summary>
        PagedResult<BookViewModel> GetBookList(BookListViewModel model, int userId);

        /// <summary>
        /// Deletes a book by its unique ID.
        /// </summary>
        void DeleteBookById(int bookId);

        /// <summary>
        /// Inserts a new book or updates an existing book based on the provided model.
        /// </summary>
        void UpsertBook(BooksUpsertViewModel model);

        /// <summary>
        /// Gets the list of all the Genres 
        /// </summary>
        List<GenreViewModel> GetGenreList();

    }
}
