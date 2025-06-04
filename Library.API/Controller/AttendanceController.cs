using Library.Business.Provider.Attendance;
using Library.Business.Provider.WorkContext;
using Library.Models.Attendance;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controller
{
    [Route("attendance")]
    [ApiController]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceProvider _attendanceProvider;
        private readonly IWorkContext _workContext;

        public AttendanceController(IAttendanceProvider attendanceProvider, IWorkContext workContext)
        {
            _attendanceProvider = attendanceProvider;
            _workContext = workContext;
        }

        /// <summary>
        /// Method to mark student's attendance.
        /// </summary>
        /// <param name="request">data type of AttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance([FromBody] AttendanceRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _attendanceProvider.MarkAttendanceAsync(request);
                    return Ok(new { Message = Messages.Attendance.AttendanceMarked });
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to approve student's marked attendance.
        /// </summary>
        /// <param name="request">data type of ApproveAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveAttendance([FromBody] ApproveAttendanceRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _attendanceProvider.ApproveAttendanceAsync(request);
                    return Ok(new { Message = Messages.Attendance.AttendanceApproved });
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to reject student's marked attendance.
        /// </summary>
        /// <param name="request">data type of RejectAttendanceRequest</param>
        /// <returns>Success or Error message</returns>
        [HttpPost("reject")]
        public async Task<IActionResult> RejectAttendance([FromBody] RejectAttendanceRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _attendanceProvider.RejectAttendanceAsync(request);
                    return Ok(new { Message = Messages.Attendance.AttendanceRejected });
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "Student")]
        [HttpPost("apply-leave")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentId = _workContext.CurrentUserId;
                    await _attendanceProvider.ApplyStudentLeaveAsync(studentId, model);
                    return Ok(new { message = "Leave applied successfully." });
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
