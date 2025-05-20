using Library.Business.ViewModel;
using Library.Models.Books;
using Library.Services.Book;
using Library.Business.Provider.WorkContext;
using Library.Utilities.Constants;

namespace Library.Business.Provider.Book
{
    public class BookProvider : IBookProvider
    {
        private readonly IBookService _bookService;
        private readonly IWorkContext _workContext;

        public BookProvider(IBookService bookService, IWorkContext workContext)
        {
            _bookService = bookService;
            _workContext = workContext;
        }

        /// <summary>
        /// Retrieves a paginated list of books with optional filtering by name and author.
        /// </summary>
        /// <param name="model">Pagination, sorting, and filter options for retrieving books.</param>
        /// <param name="userId">ID of the current user requesting the data.</param>
        /// <returns>A paginated result containing book view models.</returns>
        public PagedResult<BookViewModel> GetBookList(BookListViewModel model, int userId)
        {
            try
            {               
                if (userId <= 0)
                {
                    throw new UnauthorizedAccessException();
                }
                var requestModel = new BookList
                {
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                    Name = model.Filters.Name?.Trim(),
                    Author = model.Filters.Author?.Trim()
                };

                var (books, totalCount) = _bookService.GetBookList(requestModel, userId);

                var bookViewModels = books.Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    Genre = b.Genre,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    AddedBy = b.AddedBy,
                    HasIssued = b.HasIssued,
                    IssueId = b.IssueId,
                    IssuedUserCount = b.IssuedUserCount
                }).ToList();

                var pageCount = (int)Math.Ceiling((double)totalCount / requestModel.PageSize);

                return new PagedResult<BookViewModel>
                {
                    Items = bookViewModels,
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize,
                    TotalCount = totalCount,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                };
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a book by its unique ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        /// <exception cref="ArgumentException">Thrown if the book ID is invalid.</exception>
        public void DeleteBookById(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException(Messages.Book.InValidBookId);

            _bookService.DeleteBookById(bookId);
        }

        /// <summary>
        /// Inserts a new book or updates an existing book based on the provided model.
        /// </summary>
        /// <param name="model">The book data to insert or update.</param>
        /// <exception cref="ArgumentNullException">Thrown if the model is null.</exception>
        public void UpsertBook(BooksUpsertViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            int currentUserId = _workContext.CurrentUserId;
            _bookService.UpsertBook(model, currentUserId);
        }

    }
}
