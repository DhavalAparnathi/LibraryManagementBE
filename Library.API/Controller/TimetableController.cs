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
        [Authorize(Roles = "Admin, HOD,Teacher, Assistant Teacher, Student")]
        [HttpGet("get-days-of-week")]
        public BaseResponse GetDaysOfWeekList()
        {
            try
            {
                var genres = _timetableProvider.GetDaysOfWeek();
                return ApiSuccess(APIStatusCode.Ok, Messages.Timetable.DaysOfWeekFetchSuccess, genres);
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
                    return ApiSuccess(APIStatusCode.Ok, Messages.Timetable.TimetableSavedSuccess);
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
                return Ok(new { Message = Messages.Timetable.TimetableDeleteSuccess });
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "HOD")]
        [HttpGet("{departmentId}/get-timetable")]
        public IActionResult GetDepartmentTimeTable(int departmentId)
        {
            try
            {
                var timetable = _timetableProvider.GetDepartmentTimeTable(departmentId);
                return Ok(timetable);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "HOD")]
        [HttpPost("upsert-timetable")]
        public async Task<IActionResult> UpsertTimeTable([FromBody] TimeTableRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var timeTableId = await _timetableProvider.UpsertTimeTableWithSlotsAsync(request);
                    return Ok(new { TimeTableId = timeTableId });
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "HOD")]
        [HttpDelete("async/{timeTableId:int}")]
        public async Task<IActionResult> DeleteTimeTableAsync(int timeTableId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleted = await _timetableProvider.DeleteTimeTableAsync(timeTableId);
                    if (deleted) return Ok(new { Message = Messages.Timetable.TimetableDeleteSuccess });
                    return NotFound(new { Message = Messages.Timetable.TimetableNotFound });
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }
    }
}
