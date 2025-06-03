using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;

namespace Library.Business.Provider
{
    public interface ITimetableProvider
    {
        List<DaysOfWeek> GetDaysOfWeek();

        void UpsertTimeTable(UpsertTimeTableViewModel model, int userId);

        void DeleteTimeTable(int timeTableId);

        IEnumerable<TimeTableSlotViewModel> GetDepartmentTimeTable(int departmentId);

        Task<int> UpsertTimeTableWithSlotsAsync(TimeTableRequest request);

        Task<bool> DeleteTimeTableAsync(int timeTableId);
    }
}
