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
            public static string PasswordResetSuccess = "Password reset successful.";
            public static string InvalidOldPassword = "Old Password is invalid.";
        }

        public static class Attendance
        {
            public const string AttendanceMarked = "Attendance marked successfully.";
            public const string AttendanceApproved = "Attendance approved successfully.";
            public const string AttendanceRejected = "Attendance rejected successfully.";
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
            public static string BookDeleteError = "Can not delete the book because it is currently issued to a user.";
            public static string BookDeleteSqlError = "Can not delete the book as it has active borrow records.";
        }

        public static class User
        {
            public static string InValidUserId = "Invalid user ID.";
            public static string UserDeleteSuccess = "User deleted successfully.";
            public static string UserAddedSuccess = "User added successfully.";
            public static string UserUpdateSuccess = "User updated successfully.";
            public static string UserFetchedSuccess = "User fetched successfully.";
            public static string UserNotFound = "User not found.";
            public static string RolesFetchSuccess = "Roles fetched successfully.";
            public static string MissingUserId = "Invalid or missing user Id in token.";
            public static string AlreadyHasHOD = "This department already has a HOD assigned.";
            public static string NoAuthorizedToDelete = "You are not authorized to delete this user.";
            public static string HODPermissions = "HOD can only manage Teacher, Assistant Teacher, and Student.";
            public static string HODNoPermission = "HOD can only edit/delete users they created.";
            public static string TeacherPermissions = "Teacher can only manage Assistant Teacher and Student.";
            public static string TeacherNoPermission = "Teacher can only edit/delete users they created.";
            public static string GeneralNoPermission = "Your role is not authorized to perform this action.";
        }

        public static class Department
        {
            public const string InvalidDepartmentId = "Invalid department ID.";
            public const string DepartmentDeleteSuccess = "Department deleted successfully.";
            public const string DepartmentAddSuccess = "Department added successfully.";
            public const string DepartmentUpdateSuccess = "Department updated successfully.";
            public const string DepartmentListFetchSuccess = "Department list fetched successfully.";
            public const string DepartmentDeleteError = "Department delete error.";
            public const string DepartmentDeleteSqlError = "Error in deleting department.";
        }

        public static class Subject
        {
            public const string InvalidSubjectId = "Invalid subject ID.";
            public const string SubjectListFetchSuccess = "Subject list fetched successfully.";
            public const string SubjectAddSuccess = "Subject added successfully.";
            public const string SubjectUpdateSuccess = "Subject updated successfully.";
            public const string SubjectDeleteSuccess = "Subject deleted successfully.";
            public const string SubjectDeleteSqlError = "Error in deleting subject.";
            public const string SubjectDeleteError = "Subject delete error.";
            public const string SubjectFetchSuccess = "Subject fetched successfully.";
        }

        public static class Timetable
        {
            public const string DaysOfWeekFetchSuccess = "Days of week saved successfully.";
            public const string TimetableSavedSuccess = "Timetable saved successfully.";
            public const string TimetableDeleteSuccess = "TimeTable deleted successfully.";
            public const string TimetableNotFound = "Timetable not found.";
            public const string UnauthorizedToManageDepartment = "You are not authorized to manage this department's timetable.";
        }

        public static class Role
        {
            public static string ADMIN = "Admin";
            public static string HOD = "HOD";
            public static string TEACHER = "Teacher";
            public static string ASSISTANT_TEACHER = "AssistantTeacher";
            public static string STUDENT = "Student";
        }

        public static class RoleIds
        {
            public const int Admin = 1;
            public const int HOD = 2;
            public const int Teacher = 3;
            public const int AssistantTeacher = 4;
            public const int Student = 5;
        }

    }
}
