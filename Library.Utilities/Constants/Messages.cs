namespace Library.Utilities.Constants
{
    public static class Messages
    {
        public static class Authentication
        {
            public static string LoggedInSuccessfully = "Login successful.";
            public static string RegisteredSuccessfully = "Registration successful.";
            public static string SameEmailAlreadyExist = "A user with this email already exists.";
            public static string InvalidCredentials = "Invalid credentials.";
            public static string InvalidAuthentication = "Authentication may be missing or invalid.";
        }

        public static class Book
        {
            public static string InValidBookId = "Invalid book ID.";
            public static string BookDeleteSuccess = "Book deleted successfully.";
            public static string BookAddedSuccess = "Book added successfully.";
            public static string BookUpdateSuccess = "Book updated successfully.";
            public static string BookFetchSuccess = "Issued books fetched successfully.";
            public static string BookIssuedSuccess = "Book issued successfully.";
            public static string BookReturnedSuccess = "Book returned successfully.";
            public static string GenreListSuccess = "Genre list retrieved successfully.";
            public static string BookDeleteError = "Cannot delete the book because it is currently issued to a user.";
            public static string BookDeleteSqlError = "Cannot delete the book as it has active borrow records.";
        }

        public static class User
        {
            public static string InValidUserId = "Invalid user ID.";
            public static string UserDeleteSuccess = "User deleted successfully.";
            public static string UserAddedSuccess = "User added successfully.";
            public static string UserUpdateSuccess = "User updated successfully.";
            public static string UserFetchedSuccess = "User fetched successfully.";
            public static string UserNotFound = "User not found.";
        }

        public static class Role
        {
            public static string USER = "User";
            public static string ADMIN = "Admin";
        }
    }
}
