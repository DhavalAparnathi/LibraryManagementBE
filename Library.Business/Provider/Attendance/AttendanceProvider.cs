using Library.Models.Attendance;
using Library.Services.Attendance;

namespace Library.Business.Provider.Attendance
{
    public class AttendanceProvider : IAttendanceProvider
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceProvider(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Method to mark student's attendance.
        /// </summary>
        /// <param name="request">data type of AttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task MarkAttendanceAsync(AttendanceRequest request)
        {
            await _attendanceService.MarkAttendanceAsync(request);
        }

        /// <summary>
        /// Method to approve student's marked attendance.
        /// </summary>
        /// <param name="request">data type of ApproveAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task ApproveAttendanceAsync(ApproveAttendanceRequest request)
        {
            await _attendanceService.ApproveAttendanceAsync(request);
        }

        /// <summary>
        /// Method to reject student's marked attendance.
        /// </summary>
        /// <param name="request">data type of RejectAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task RejectAttendanceAsync(RejectAttendanceRequest request)
        {
            await _attendanceService.RejectAttendanceAsync(request);
        }

        public async Task ApplyStudentLeaveAsync(int studentId, LeaveRequestModel model)
        {
            await _attendanceService.ApplyStudentLeaveAsync(studentId, model.SubjectId, model.Date, model.Reason);

        }
    }
}
