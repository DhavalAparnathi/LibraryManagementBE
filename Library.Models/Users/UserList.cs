namespace Library.Models.Users
{
    public class UserList : PaginationModel
    {
        public string? UserName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
    }
}
