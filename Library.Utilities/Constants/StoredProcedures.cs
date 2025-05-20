namespace Library.Utilities.Constants
{
    // Stored procedure stored in static public accessible with the Name
    public static class StoredProcedures 
    {
        public static string GetAllUsers => "GetAllUsers";

        public static string GetAllBooks => "GetAllBooks";

        public static string DeleteBookById => "DeleteBookById";

        public static string DeleteUserById => "DeleteUserById";

        public static string GetUserById => "GetUserById";

        public static string UpsertBook => "UpsertBook";

        public static string UpsertUser => "UpsertUser";

        public static string IssueBook => "IssueBook";

        public static string ReturnBook => "ReturnBook";

        public static string GetUserByEmail => "GetUserByEmail";

        public static string RegisterUser => "RegisterUser";

        public static string GetIssuedBooksByUserId => "GetIssuedBooksByUserId";

        public static string IsEmailExists => "IsEmailExists";

        public const string GetAllGenres = "GetAllGenres";

    }
}
