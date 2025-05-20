using Dapper;
using Library.Data.Repository;
using Library.Utilities.Constants;

namespace Library.Services.IssueService
{
    public class IssueService : IIssueService
    {
        private readonly IDapperService _dapperService;

        public IssueService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Retrieves all books currently issued to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose issued books are to be retrieved.</param>
        /// <returns>A list of issued book details associated with the user.</returns>
        public List<IssuedBookDetailViewModel> GetIssuedBooksByUserId(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);

            return _dapperService.Query<IssuedBookDetailViewModel>(StoredProcedures.GetIssuedBooksByUserId, parameters).ToList();
        }

        /// <summary>
        /// Issues a book to a user.
        /// </summary>
        /// <param name="model">The details required to issue a book, including BookId and UserId.</param>
        public void IssueBook(IssueBookViewModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("BookId", model.BookId);
            parameters.Add("UserId", model.UserId);

            _dapperService.Execute(StoredProcedures.IssueBook, parameters);
        }

        /// <summary>
        /// Marks a previously issued book as returned.
        /// </summary>
        /// <param name="model">The details required to return a book, including the IssueId.</param>
        public void ReturnBook(ReturnBookViewModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("IssueId", model.IssueId);

            _dapperService.Execute(StoredProcedures.ReturnBook, parameters);
        }
    }
}
