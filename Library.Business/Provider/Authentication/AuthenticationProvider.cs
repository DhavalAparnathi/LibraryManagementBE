using Library.Business.ViewModel.Authentication;
using Library.Services.JwtToken;
using Library.Services.Authentication;
using Library.Business.ViewModel;
using Library.Utilities.Constants;

namespace Library.Business.Provider.Authentication
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IAuthenticationService _authService;
        private readonly IJwtTokenHelper _jwtHelper;

        public AuthenticationProvider(IAuthenticationService authService, IJwtTokenHelper jwtHelper)
        {
            _authService = authService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Log in method verifies the user role & returns data if credentials are correct
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success response with JWT Token</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public AuthResponseViewModel Login(AuthenticationViewModel model)
        {
            // Validate the user by id pass whether exist in DB or not
            var user = _authService.ValidateUser(model.Email, model.Password); 

            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException(Messages.Authentication.InvalidCredentials);
            }

            // Generate a role based JWT Token based on inserted valid credentials
            var token = _jwtHelper.GenerateToken(user); 

            return new AuthResponseViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }

        /// <summary>
        /// Register method that takes user input values & creates a user with entered data.
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Register(RegisterViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _authService.Register(model);
        }

    }
}
