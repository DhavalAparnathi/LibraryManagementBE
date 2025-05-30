namespace Library.Models.Users
{
    public class Users
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime JoinedDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatedBy { get; set; }

        public int DepartmentId { get; set; }

    }
}
