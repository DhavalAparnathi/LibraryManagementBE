using Library.Business.ViewModel;
using Library.Models.Users;

namespace Library.Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Validates the user for the role & the credentials.
        /// </summary>
        Users ValidateUser(string email, string password);

        /// <summary>
        /// Register method that takes user input values & creates a user with entered data.
        /// </summary>
        void Register(RegisterViewModel model);

    }
}
