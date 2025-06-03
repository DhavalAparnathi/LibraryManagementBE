using Library.Business.Provider.Authentication;
using Library.Business.ViewModel;
using Library.Business.ViewModel.Authentication;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;
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
        /// Method that resets user password based on confirmation of old password & new password.
        /// </summary>
        /// <returns>Generates a new password.</returns>
        [HttpPost("reset-password")]
        public BaseResponse ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authProvider.ResetPassword(model);
                    return ApiSuccess(APIStatusCode.Ok, Messages.Authentication.PasswordResetSuccess);
                }
                throw new DataValidationException(ModelState);
            }
            catch (Exception ex)
            {
                return ApiError(APIStatusCode.ServerError, ex.Message);
            }

        }

    }
}

