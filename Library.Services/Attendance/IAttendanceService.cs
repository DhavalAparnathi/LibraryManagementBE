using Library.Models.Attendance;

namespace Library.Services.Attendance
{
    public interface IAttendanceService
    {
        Task MarkAttendanceAsync(AttendanceRequest request);

        Task ApproveAttendanceAsync(ApproveAttendanceRequest request);

        Task RejectAttendanceAsync(RejectAttendanceRequest request);

        Task ApplyStudentLeaveAsync(int studentId, int subjectId, DateTime date, string reason);
    }
}
