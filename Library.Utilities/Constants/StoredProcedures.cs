namespace Library.Utilities.Constants
{
    // Stored procedure stored in static public accessible with the Name
    public static class StoredProcedures
    {
        public static string GetAllUsers => "GetAllUsers";

        public static string GetAllRoles => "GetAllRoles";

        public static string GetUsersByRole => "GetUsersByRole";

        public static string GetAllBooks => "GetAllBooks";

        public static string DeleteBookById => "DeleteBookById";

        public static string DeleteUserById => "DeleteUserById";

        public static string GetUserById => "GetUserById";

        public static string GetDepartmentById => "GetDepartmentById";

        public static string UpsertBook => "UpsertBook";

        public static string UpsertUser => "UpsertUser";

        public static string IssueBook => "IssueBook";

        public static string ReturnBook => "ReturnBook";

        public static string GetUserByEmail => "GetUserByEmail";

        public static string RegisterUser => "RegisterUser";

        public static string GetIssuedBooksByUserId => "GetIssuedBooksByUserId";

        public static string IsEmailExists => "IsEmailExists";

        public static string GetAllGenres => "GetAllGenres";

        public static string GetAllDaysOfWeek => "GetAllDaysOfWeek";

        public static string ValidateUser => "ValidateUser";

        public static string ResetUserPassword => "ResetUserPassword";

        public static string GetAllDepartments => "GetAllDepartments";

        public static string GetAllDepartmentsList => "GetAllDepartmentsList";

        public static string DeleteDepartmentById => "DeleteDepartmentById";

        public static string UpsertDepartment => "UpsertDepartment";

        public static string GetAllSubjects => "GetAllSubjects";

        public static string GetAllSubjectList => "GetAllSubjectList";

        public static string DeleteSubjectById => "DeleteSubjectById";

        public static string UpsertSubject => "UpsertSubject";

        public static string GetSubjectsByDepartment => "GetSubjectsByDepartment";

        public static string GetSubjectById => "GetSubjectById";

        public static string CheckUserCreatedBy => "CheckUserCreatedBy";

        public static string UpsertTimeTable => "UpsertTimeTable";

        public static string DeleteTimeTableById => "DeleteTimeTableById";

        public static string GetFullTimeTableByDepartmentId => "GetFullTimeTableByDepartmentId";

    }
}
