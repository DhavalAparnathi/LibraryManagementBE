using Dapper;
using Library.Data.Repository;
using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using Library.Utilities.Constants;

namespace Library.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly IDapperService _dapperService;

        public TimetableService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Gets all the days of week.
        /// </summary>
        /// <returns>List of all the days of the week.</returns>
        public List<DaysOfWeek> GetAllDaysOfWeek()
        {
            var genres = _dapperService.Query<DaysOfWeek>(StoredProcedures.GetAllDaysOfWeek);
            return genres.ToList();
        }

        public void UpsertTimeTable(UpsertTimeTableRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TimeTableId", request.TimeTableId);
            parameters.Add("DepartmentId", request.DepartmentId);
            parameters.Add("StartDate", request.StartDate);
            parameters.Add("EndDate", request.EndDate);
            parameters.Add("CreatedBy", request.CreatedBy);

            _dapperService.Execute(StoredProcedures.UpsertTimeTable, parameters);
        }

        public void DeleteTimeTableById(int timeTableId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TimeTableId", timeTableId);

            _dapperService.Execute(StoredProcedures.DeleteTimeTableById, parameters);
        }

        public IEnumerable<TimeTableSlotViewModel> GetFullTimeTableByDepartmentId(int departmentId)
        {
            var param = new { DepartmentId = departmentId };
            return _dapperService.Query<TimeTableSlotViewModel>(StoredProcedures.GetFullTimeTableByDepartmentId, param);
        }
    }
}
