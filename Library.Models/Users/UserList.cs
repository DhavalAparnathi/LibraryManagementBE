namespace Library.Models.Users
{
    public class UserList : PaginationModel
    {
        public string? UserName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string CurrentUserRole { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
    }

    public class UserListViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
