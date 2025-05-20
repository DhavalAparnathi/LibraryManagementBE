using Library.Business.Provider.Authentication;
using Library.Business.ViewModel;
using Library.Business.ViewModel.Authentication;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [ApiController]
    [Route("authorize")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationProvider _authProvider;

        public AuthenticationController(IAuthenticationProvider authProvider)
        {
            _authProvider = authProvider;
        }

        /// <summary>
        /// Log in method verifies the user role & returns success responsive with data if credentials are correct
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success response with JWT Token</returns>
        /// <exception cref="DataValidationException">Unauthorized error for invalid credentials</exception>
        [HttpPost("login")]
        public BaseResponse Login([FromBody] AuthenticationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var authResult = _authProvider.Login(model);
                    return ApiSuccess(APIStatusCode.Ok, Messages.Authentication.LoggedInSuccessfully, authResult);
                }
                throw new DataValidationException(ModelState);
            }

            catch (UnauthorizedAccessException ex)
            {
                return ApiError(APIStatusCode.UnAuthorized, ex.Message);
            }
        }

        /// <summary>
        /// Register method that takes user input values & returns success response & creates a user with entered data
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns success response & creates a user</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public BaseResponse RegisterUser([FromBody] RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authProvider.Register(model);
                    return ApiSuccess(APIStatusCode.Ok, Messages.Authentication.RegisteredSuccessfully);
                }

                throw new DataValidationException(ModelState);
            }
            catch (SqlException ex) when (ex.Message.Contains(Messages.Authentication.SameEmailAlreadyExist))
            {
                return ApiError(APIStatusCode.Conflict, Messages.Authentication.SameEmailAlreadyExist);
            }
            catch 
            {
                throw;
            }
        }

    }
}

