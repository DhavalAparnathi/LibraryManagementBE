using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using Library.Services;
using Library.Services.Departments;
using Library.Utilities.Constants;

namespace Library.Business.Provider.Timetable
{
    public class TimetableProvider : ITimetableProvider
    {
        private readonly ITimetableService _timetableService;
        private readonly IDepartmentService _departmentService;

        public TimetableProvider(ITimetableService timetableService, IDepartmentService departmentService)
        {
            _timetableService = timetableService;
            _departmentService = departmentService;
        }

        /// <summary>
        /// Gets all the days of week.
        /// </summary>
        /// <returns>List of all the days of the week.</returns>
        public List<DaysOfWeek> GetDaysOfWeek()
        {
            return _timetableService.GetAllDaysOfWeek();
        }

        /// <summary>
        /// Method to add or edit the Timetable.
        /// </summary>
        /// <param name="model">model type of UpsertTimeTableViewModel</param>
        /// <param name="userId">current userId</param>
        /// <exception cref="UnauthorizedAccessException">Unauthorized exception if current user doesn't have the permission.</exception>
        public void UpsertTimeTable(UpsertTimeTableViewModel model, int userId)
        {
            if (userId <= 0)
                throw new UnauthorizedAccessException();

            // Check if logged in user is the HOD of the department
            var hodUserId = _departmentService.GetHodUserIdByDepartment(model.DepartmentId);
            if (hodUserId != userId)
                throw new UnauthorizedAccessException(Messages.Timetable.UnauthorizedToManageDepartment);

            var request = new UpsertTimeTableRequest
            {
                TimeTableId = model.TimeTableId,
                DepartmentId = model.DepartmentId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedBy = userId,
            };

            _timetableService.UpsertTimeTable(request);
        }

        /// <summary>
        /// Method that handles the Delete timetable by Id.
        /// </summary>
        /// <param name="timeTableId">Timetable Id which needs to be deleted.</param>
        public void DeleteTimeTable(int timeTableId)
        {
            _timetableService.DeleteTimeTableById(timeTableId);
        }

        /// <summary>
        /// Method that retrieves the data of department timetable.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>Details of timetable of a department</returns>
        public IEnumerable<TimeTableSlotViewModel> GetDepartmentTimeTable(int departmentId)
        {
            return _timetableService.GetFullTimeTableByDepartmentId(departmentId);

        }

        /// <summary>
        /// Method that upserts the timetable with the slots.
        /// </summary>
        /// <param name="request">type of TimeTableRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task<int> UpsertTimeTableWithSlotsAsync(TimeTableRequest request)
        {
            return await _timetableService.UpsertTimeTableWithSlotsAsync(request);
        }

        /// <summary>
        /// Method that handles the delete functionality of the timetable.
        /// </summary>
        /// <param name="timeTableId">Timetable Id which needed to be deleted.</param>
        /// <returns>Success or error response.</returns>
        public async Task<bool> DeleteTimeTableAsync(int timeTableId)
        {
            return await _timetableService.DeleteTimeTableAsync(timeTableId);
        }
    }
}
