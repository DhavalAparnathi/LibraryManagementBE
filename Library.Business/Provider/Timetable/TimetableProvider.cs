using Library.Models.DaysOfWeek;
using Library.Services;

namespace Library.Business.Provider.Timetable
{
    public class TimetableProvider: ITimetableProvider
    {
        private readonly ITimetableService _timetableService;

        public TimetableProvider(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        public List<DaysOfWeek> GetDaysOfWeek()
        {
            return _timetableService.GetAllDaysOfWeek();
        }
    }
}
