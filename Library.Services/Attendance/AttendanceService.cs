using Dapper;
using Library.Models.Attendance;
using Library.Utilities.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Library.Services.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly string _connectionString;

        public AttendanceService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Method to mark student's attendance.
        /// </summary>
        /// <param name="request">data type of AttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task MarkAttendanceAsync(AttendanceRequest request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@IsPresent", request.IsPresent, DbType.Boolean);
            parameters.Add("@FilledBy", request.FilledBy, DbType.Int32);
            parameters.Add("@IsTeacher", request.IsTeacher ? 1 : 0, DbType.Int32);

            await connection.ExecuteAsync(StoredProcedures.MarkAttendance, parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Method to approve student's marked attendance.
        /// </summary>
        /// <param name="request">data type of ApproveAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task ApproveAttendanceAsync(ApproveAttendanceRequest request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@ApprovedBy", request.ApprovedBy, DbType.Int32);

            await connection.ExecuteAsync(StoredProcedures.ApproveAttendance, parameters, 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Method to reject student's marked attendance.
        /// </summary>
        /// <param name="request">data type of RejectAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task RejectAttendanceAsync(RejectAttendanceRequest request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@StudentId", request.StudentId, DbType.Int32);
            parameters.Add("@SubjectId", request.SubjectId, DbType.Int32);
            parameters.Add("@Date", request.Date.Date, DbType.Date);
            parameters.Add("@RejectedBy", request.RejectedBy, DbType.Int32);
            parameters.Add("@Reason", request.Reason, DbType.String);

            await connection.ExecuteAsync(StoredProcedures.RejectAttendance, parameters,
                commandType: CommandType.StoredProcedure);
        }

    }
}
