using Dapper;
using Library.Business.ViewModel;
using Library.Data.Repository;
using Library.Models.Books;
using Library.Utilities.Constants;

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
        public (List<Books> Books, int TotalCount) GetBookList(BookList model, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", model.SortColumn);
            parameters.Add("SortDirection", model.SortDirection);
            parameters.Add("Name", model.Name);
            parameters.Add("Author", model.Author);
            parameters.Add("Genre", model.Genre);
            parameters.Add("UserId", userId);

            var (books, totalCount) = _dapperService.QueryMultiple<Books, int>(StoredProcedures.GetAllBooks, parameters);

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

            _dapperService.Execute(StoredProcedures.DeleteBookById, parameters);
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
            parameters.Add("Genre", model.Genre);
            parameters.Add("TotalCopies", model.TotalCopies);
            parameters.Add("AvailableCopies", model.AvailableCopies);
            parameters.Add("AddedBy", addedBy);

            _dapperService.Execute(StoredProcedures.UpsertBook, parameters);
        }

    }
}
