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

        /// <summary>
        /// Gets all the days of week.
        /// </summary>
        /// <returns>List of all the days of the week.</returns>
        public List<DaysOfWeek> GetDaysOfWeek()
        {
            return _timetableService.GetAllDaysOfWeek();
        }
    }
}
