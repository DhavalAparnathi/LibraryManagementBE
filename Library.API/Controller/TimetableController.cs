using Library.Business.Provider;
using Library.Business.ViewModel;
using Library.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("timetable")]
    [ApiController]
    public class TimetableController : BaseController
    {
        private readonly ITimetableProvider _timetableProvider;

        public TimetableController(ITimetableProvider timetableProvider)
        {
            _timetableProvider = timetableProvider;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("get-days-of-week")]
        public BaseResponse GetDaysOfWeekList()
        {
            try
            {
                var genres = _timetableProvider.GetDaysOfWeek();
                return ApiSuccess(APIStatusCode.Ok, Messages.Book.GenreListSuccess, genres);
            }
            catch
            {
                throw;
            }
        }
    }
}
