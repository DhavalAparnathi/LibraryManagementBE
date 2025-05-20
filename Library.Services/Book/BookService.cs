using Dapper;
using Library.Business.ViewModel;
using Library.Data.Repository;
using Library.Models.Books;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.Data.SqlClient;

namespace Library.Services.Book
{
    public class BookService : IBookService
    {
        private readonly IDapperService _dapperService;

        public BookService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Retrieves a paginated list of books from the database based on filter and sort criteria.
        /// </summary>
        /// <param name="model">Model containing pagination, filter, and sort options.</param>
        /// <param name="userId">The ID of the user requesting the book list.</param>
        /// <returns>A tuple containing the list of books and the total count.</returns>
        public (List<BookWithGenre> Books, int TotalCount) GetBookList(BookList model, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", model.SortColumn);
            parameters.Add("SortDirection", model.SortDirection);
            parameters.Add("Name", model.Name);
            parameters.Add("Author", model.Author);
            parameters.Add("GenreId", model.GenreId);
            parameters.Add("UserId", userId);

            var (books, totalCount) = _dapperService.QueryMultiple<BookWithGenre, int>(StoredProcedures.GetAllBooks, parameters);

            return (books, totalCount);
        }

        /// <summary>
        /// Deletes a book record from the database based on the book ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        public void DeleteBookById(int bookId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", bookId);

            try
            {
                _dapperService.Execute(StoredProcedures.DeleteBookById, parameters);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains(Messages.Book.BookDeleteSqlError))
                {
                    throw new DataValidationException(Messages.Book.BookDeleteError);
                }
                throw;
            }
        }

        /// <summary>
        /// Inserts or updates a book record in the database.
        /// </summary>
        /// <param name="model">The book data to insert or update.</param>
        /// <param name="addedBy">The ID of the user adding or updating the book.</param>
        public void UpsertBook(BooksUpsertViewModel model, int addedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", model.Id);
            parameters.Add("Name", model.Name);
            parameters.Add("Author", model.Author);
            parameters.Add("GenreId", model.GenreId);
            parameters.Add("TotalCopies", model.TotalCopies);
            parameters.Add("AvailableCopies", model.AvailableCopies);
            parameters.Add("AddedBy", addedBy);

            _dapperService.Execute(StoredProcedures.UpsertBook, parameters);
        }

        public List<GenreViewModel> GetAllGenres()
        {
            var genres = _dapperService.Query<GenreViewModel>(StoredProcedures.GetAllGenres);
            return genres.ToList();
        }
    }
}
