using Library.Models.Attendance;

namespace Library.Business.Provider.Attendance
{
    public interface IAttendanceProvider
    {
        Task MarkAttendanceAsync(AttendanceRequest request);

        Task ApproveAttendanceAsync(ApproveAttendanceRequest request);

        Task RejectAttendanceAsync(RejectAttendanceRequest request);

        Task ApplyStudentLeaveAsync(int studentId, LeaveRequestModel model);
    }
}
