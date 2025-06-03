using Dapper;
using Library.Data.Repository;
using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using Library.Utilities.Constants;
using System.Data;

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

        /// <summary>
        /// Method to add or edit the Timetable.
        /// </summary>
        /// <param name="model">request type of UpsertTimeTableRequest</param>
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

        /// <summary>
        /// Method that handles the Delete timetable by Id.
        /// </summary>
        /// <param name="timeTableId">Timetable Id which needs to be deleted.</param>
        public void DeleteTimeTableById(int timeTableId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TimeTableId", timeTableId);

            _dapperService.Execute(StoredProcedures.DeleteTimeTableById, parameters);
        }

        /// <summary>
        /// Method that retrieves the data of department timetable.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>Details of timetable of a department</returns>
        public IEnumerable<TimeTableSlotViewModel> GetFullTimeTableByDepartmentId(int departmentId)
        {
            var param = new { DepartmentId = departmentId };
            return _dapperService.Query<TimeTableSlotViewModel>(StoredProcedures.GetFullTimeTableByDepartmentId, param);
        }

        /// <summary>
        /// Method that upserts the timetable with the slots.
        /// </summary>
        /// <param name="request">type of TimeTableRequest</param>
        /// <returns>Success or Error message</returns>
        public async Task<int> UpsertTimeTableWithSlotsAsync(TimeTableRequest request)
        {
            var slotsTable = new DataTable();
            slotsTable.Columns.Add("SlotId", typeof(int));
            slotsTable.Columns.Add("DayId", typeof(int));
            slotsTable.Columns.Add("Period", typeof(int));
            slotsTable.Columns.Add("SubjectId", typeof(int));
            slotsTable.Columns.Add("TeacherId", typeof(int));
            slotsTable.Columns.Add("AssistantTeacherId", typeof(int));

            foreach (var slot in request.Slots)
            {
                slotsTable.Rows.Add(slot.SlotId, slot.DayId, slot.Period, slot.SubjectId, slot.TeacherId, slot.AssistantTeacherId);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@TimeTableId", request.TimeTableId, DbType.Int32, ParameterDirection.InputOutput);
            parameters.Add("@DepartmentId", request.DepartmentId);
            parameters.Add("@StartDate", request.StartDate);
            parameters.Add("@EndDate", request.EndDate);
            parameters.Add("@CreatedBy", request.CreatedBy);
            parameters.Add("@Slots", slotsTable.AsTableValuedParameter(StoredProcedures.TimeTableSlotType));

            return await _dapperService.ExecuteWithOutputAsync(StoredProcedures.UpsertTimeTableWithSlots, parameters, "@TimeTableId");
        }

        /// <summary>
        /// Method that handles the delete functionality of the timetable.
        /// </summary>
        /// <param name="timeTableId">Timetable Id which needed to be deleted.</param>
        /// <returns>Success or error response.</returns>
        public async Task<bool> DeleteTimeTableAsync(int timeTableId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TimeTableId", timeTableId);
            var result = await Task.Run(() => _dapperService.Execute(StoredProcedures.DeleteTimeTable, parameters));
            return result > 0;
        }
    }
}
