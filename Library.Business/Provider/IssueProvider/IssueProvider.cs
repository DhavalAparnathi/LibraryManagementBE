using Library.Services.IssueService;

namespace Library.Business.Provider
{
    public class IssueProvider : IIssueProvider
    {
        private readonly IIssueService _issueService;

        public IssueProvider(IIssueService issueService)
        {
            _issueService = issueService;
        }

        /// <summary>
        /// Retrieves a list of books issued by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of issued book details.</returns>
        public List<IssuedBookDetailViewModel> GetIssuedBooksByUserId(int userId)
        {
            return _issueService.GetIssuedBooksByUserId(userId);
        }

        /// <summary>
        /// Issues a book to a user.
        /// </summary>
        /// <param name="model">The model containing issue details such as user ID and book ID.</param>
        public void IssueBook(IssueBookViewModel model)
        {
            _issueService.IssueBook(model);
        }

        /// <summary>
        /// Returns a previously issued book.
        /// </summary>
        /// <param name="model">The model containing return details such as issue ID or user/book reference.</param>
        public void ReturnBook(ReturnBookViewModel model)
        {
            _issueService.ReturnBook(model);
        }
    }
}
