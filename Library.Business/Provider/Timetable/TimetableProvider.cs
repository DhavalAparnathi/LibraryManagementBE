using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using Library.Services;
using Library.Services.Departments;
using Library.Services.User;

namespace Library.Business.Provider.Timetable
{
    public class TimetableProvider: ITimetableProvider
    {
        private readonly ITimetableService _timetableService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        public TimetableProvider(ITimetableService timetableService, IDepartmentService departmentService, IUserService userService)
        {
            _timetableService = timetableService;
            _departmentService = departmentService;
            _userService = userService;
        }

        /// <summary>
        /// Gets all the days of week.
        /// </summary>
        /// <returns>List of all the days of the week.</returns>
        public List<DaysOfWeek> GetDaysOfWeek()
        {
            return _timetableService.GetAllDaysOfWeek();
        }

        public void UpsertTimeTable(UpsertTimeTableViewModel model, int userId)
        {
            if (userId <= 0)
                throw new UnauthorizedAccessException();

            // Check if logged in user is the HOD of the department
            var hodUserId = _departmentService.GetHodUserIdByDepartment(model.DepartmentId);
            if (hodUserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to manage this department's timetable.");

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

        public void DeleteTimeTable(int timeTableId)
        {
            _timetableService.DeleteTimeTableById(timeTableId);
        }

        public IEnumerable<TimeTableSlotViewModel> GetDepartmentTimeTable(int departmentId)
        {
            return _timetableService.GetFullTimeTableByDepartmentId(departmentId);

        }
    }
}
