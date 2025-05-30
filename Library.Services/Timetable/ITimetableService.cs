using Library.Models.DaysOfWeek;
using Library.Models.TimeTables;
using Library.Models.TImeTableSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ITimetableService
    {
        List<DaysOfWeek> GetAllDaysOfWeek();

        void UpsertTimeTable(UpsertTimeTableRequest request);

        void DeleteTimeTableById(int timeTableId);

        IEnumerable<TimeTableSlotViewModel> GetFullTimeTableByDepartmentId(int departmentId);
    }
}
