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

        public List<DaysOfWeek> GetAllDaysOfWeek()
        {
            var genres = _dapperService.Query<DaysOfWeek>(StoredProcedures.GetAllDaysOfWeek);
            return genres.ToList();
        }
    }
}
