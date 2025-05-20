namespace Library.Business.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int UnreturnedBooks { get; set; }

    }
}
