using Library.Models.Attendance;

namespace Library.Services.Attendance
{
    public interface IAttendanceService
    {
        Task MarkAttendanceAsync(AttendanceRequest request);

        Task ApproveAttendanceAsync(ApproveAttendanceRequest request);

        Task RejectAttendanceAsync(RejectAttendanceRequest request);

    }
}
