public class IssueBookViewModel
{
    public int BookId { get; set; }

    public int UserId { get; set; }

}

public class ReturnBookViewModel
{
    public int IssueId { get; set; } 

}

public class IssuedBookDetailViewModel
{
    public int IssueId { get; set; }

    public string BookName { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public DateTime DateBorrowed { get; set; }

    public DateTime? DateReturned { get; set; }

    public bool IsReturned { get; set; }

}
