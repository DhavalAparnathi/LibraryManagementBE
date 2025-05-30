using Library.Business.ViewModel;
using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Provider
{
    public interface ITimetableProvider
    {
        List<DaysOfWeek> GetDaysOfWeek();

        void UpsertTimeTable(UpsertTimeTableViewModel model, int userId);

        void DeleteTimeTable(int timeTableId);

        IEnumerable<TimeTableSlotViewModel> GetDepartmentTimeTable(int departmentId);
    }
}
