namespace Library.Business.Provider
{
    public interface IIssueProvider
    {
        void IssueBook(IssueBookViewModel model);
        void ReturnBook(ReturnBookViewModel model);
        List<IssuedBookDetailViewModel> GetIssuedBooksByUserId(int userId);
    }
}
