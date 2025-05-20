using Library.Business.ViewModel;
using Library.Models.Books;

namespace Library.Business.Provider.Book
{
    public interface IBookProvider
    {
        PagedResult<BookViewModel> GetBookList(BookListViewModel model, int userId);

        void DeleteBookById(int bookId);

        void UpsertBook(BooksUpsertViewModel model);

    }
}
