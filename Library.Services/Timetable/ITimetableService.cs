using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;

namespace Library.Services
{
    public interface ITimetableService
    {
        List<DaysOfWeek> GetAllDaysOfWeek();

        void UpsertTimeTable(UpsertTimeTableRequest request);

        void DeleteTimeTableById(int timeTableId);

        IEnumerable<TimeTableSlotViewModel> GetFullTimeTableByDepartmentId(int departmentId);

        Task<int> UpsertTimeTableWithSlotsAsync(TimeTableRequest request);

        Task<bool> DeleteTimeTableAsync(int timeTableId);
    }
}
