using Library.Business.Provider;
using Library.Business.Provider.WorkContext;
using Library.Business.ViewModel;
using Library.Models.TimeTables;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("timetable")]
    [ApiController]
    public class TimetableController : BaseController
    {
        private readonly ITimetableProvider _timetableProvider;
        private readonly IWorkContext _workContext;

        public TimetableController(ITimetableProvider timetableProvider, IWorkContext workContext)
        {
            _timetableProvider = timetableProvider;
            _workContext = workContext;
        }

        /// <summary>
        /// Gets all the days of week.
        /// </summary>
        /// <returns>List of all the days of the week.</returns>
        [Authorize(Roles = "Admin, Student")]
        [HttpGet("get-days-of-week")]
        public BaseResponse GetDaysOfWeekList()
        {
            try
            {
                var genres = _timetableProvider.GetDaysOfWeek();
                return ApiSuccess(APIStatusCode.Ok, Messages.Book.GenreListSuccess, genres);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "HOD")]
        [HttpPost("upsert")]
        public BaseResponse UpsertTimeTable([FromBody] UpsertTimeTableViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int currentUserId = _workContext.CurrentUserId;

                    _timetableProvider.UpsertTimeTable(model, currentUserId);
                    return ApiSuccess(APIStatusCode.Ok, "Timetable saved successfully.");
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "HOD")]
        [HttpDelete("{timeTableId}")]
        public IActionResult DeleteTimeTable(int timeTableId)
        {
            try
            {
                _timetableProvider.DeleteTimeTable(timeTableId);
                return Ok(new { Message = "TimeTable deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error deleting timetable: {ex.Message}" });
            }
        }

        [HttpGet("{departmentId}/get-timetable")]
        public IActionResult GetDepartmentTimeTable(int departmentId)
        {
            try
            {
                var timetable = _timetableProvider.GetDepartmentTimeTable(departmentId);
                return Ok(timetable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error retrieving timetable: {ex.Message}" });
            }
        }
    }
}
