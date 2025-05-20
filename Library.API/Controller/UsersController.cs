using Library.Business.Provider.User;
using Library.Business.ViewModel;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("users")]
    public class UsersController : BaseController
    {

        private readonly IUserProvider _userProvider;

        public UsersController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        /// <summary>
        /// Retrieves a paginated list of users based on filtering and search criteria.
        /// </summary>
        /// <param name="model">User list filter and pagination view model.</param>
        /// <returns>Standardized response containing the paginated user list.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("list")]
        public BaseResponse GetUserList([FromBody]UserListVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pagedResult = _userProvider.GetUserList(model);

                    return ApiSuccess(APIStatusCode.Ok, string.Empty, pagedResult);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Standardized response indicating success or failure of the operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public BaseResponse DeleteUserById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new DataValidationException(Messages.User.InValidUserId);

                _userProvider.DeleteUserById(id);
                return ApiSuccess(APIStatusCode.Ok, Messages.User.UserDeleteSuccess);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new user or updates an existing one.
        /// </summary>
        /// <param name="model">User data for insert or update.</param>
        /// <returns>Standardized response indicating the result of the operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("upsert")]
        public BaseResponse UpsertUser([FromBody] UsersUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userProvider.UpsertUser(model);
                    return ApiSuccess(APIStatusCode.Ok, model.Id > 0 ? Messages.User.UserUpdateSuccess : Messages.User.UserAddedSuccess);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the details of a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Standardized response containing user details or an error message.</returns>
        [HttpGet("{id:int}")]
        public BaseResponse GetUserById(int id)
        {
            try
            {
                var user = _userProvider.GetUserById(id);

                if (user == null)
                    return ApiError(APIStatusCode.NotFound, Messages.User.UserNotFound);

                return ApiSuccess(APIStatusCode.Ok, Messages.User.UserFetchedSuccess, user);
            }
            catch (Exception ex)
            {
                return ApiError(APIStatusCode.ServerError, ex.Message);
            }
        }

    }
}
