namespace Library.Services.IssueService
{
    public interface IIssueService
    {

        /// <summary>
        /// Retrieves all books currently issued to a specific user.
        /// </summary>
        List<IssuedBookDetailViewModel> GetIssuedBooksByUserId(int userId);

        /// <summary>
        /// Issues a book to a user.
        /// </summary>
        void IssueBook(IssueBookViewModel model);

        /// <summary>
        /// Marks a previously issued book as returned.
        /// </summary>
        void ReturnBook(ReturnBookViewModel model);
    }
}
