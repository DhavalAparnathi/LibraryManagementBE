using Dapper;
using Library.Data.Repository;
using Library.Models.Attendance;
using Library.Utilities.Constants;
using System.Data;

namespace Library.Services.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IDapperService _dapperService;

        public AttendanceService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Method to mark student's attendance.
        /// </summary>
        /// <param name="request">data type of AttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task MarkAttendanceAsync(AttendanceRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@IsPresent", request.IsPresent, DbType.Boolean);
            parameters.Add("@FilledBy", request.FilledBy, DbType.Int32);
            parameters.Add("@IsTeacher", request.IsTeacher ? 1 : 0, DbType.Int32);

            // Use DapperService's ExecuteAsync
            await _dapperService.ExecuteAsync(StoredProcedures.MarkAttendance, parameters);
        }

        /// <summary>
        /// Method to approve student's marked attendance.
        /// </summary>
        /// <param name="request">data type of ApproveAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task ApproveAttendanceAsync(ApproveAttendanceRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@ApprovedBy", request.ApprovedBy, DbType.Int32);

            await _dapperService.ExecuteAsync(StoredProcedures.ApproveAttendance, parameters);
        }

        /// <summary>
        /// Method to reject student's marked attendance.
        /// </summary>
        /// <param name="request">data type of RejectAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task RejectAttendanceAsync(RejectAttendanceRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@RejectedBy", request.RejectedBy, DbType.Int32);
            parameters.Add("@Reason", request.Reason, DbType.String);

            await _dapperService.ExecuteAsync(StoredProcedures.RejectAttendance, parameters);
        }

        public async Task ApplyStudentLeaveAsync(int studentId, int subjectId, DateTime date, string reason)
        {
            var parameters = new DynamicParameters();

            parameters.Add("StudentId", studentId);
            parameters.Add("SubjectId", subjectId);
            parameters.Add("Date", date);
            parameters.Add("Reason", reason);

            await _dapperService.ExecuteAsync("ApplyStudentLeave", parameters);
        }
    }
}
