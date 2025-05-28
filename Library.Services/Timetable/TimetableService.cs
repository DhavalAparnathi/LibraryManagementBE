using Library.Data.Repository;
using Library.Models.DaysOfWeek;
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
    }
}
