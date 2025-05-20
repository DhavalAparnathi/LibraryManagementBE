using Dapper;
using Library.Business.ViewModel;
using Library.Data.Repository;
using Library.Models.Users;
using Library.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Library.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDapperService _dapperService;

        public AuthenticationService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Validates the user for the role & the credentials.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Success or failure based on user verification.</returns>
        public Users? ValidateUser(string email, string password)
        {
            var param = new { Email = email };

            var user = _dapperService.QueryFirstOrDefault<Users>(StoredProcedures.GetUserByEmail, param);

            if (user == null || user.IsDeleted)
                return null;

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(null, user.PasswordHash, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        /// <summary>
        /// Register method that takes user input values & creates a user with entered data.
        /// </summary>
        /// <param name="model"></param>
        public void Register(RegisterViewModel model)
        {
            var parameters = new DynamicParameters();

            string newUsername = model.Username.Replace(" ", string.Empty);

            parameters.Add("Username", newUsername);
            parameters.Add("Email", model.Email);
            parameters.Add("PhoneNumber", model.PhoneNumber);

            var hasher = new PasswordHasher<string>();
            string hashedPassword = hasher.HashPassword(null, model.Password);
            parameters.Add("PasswordHash", hashedPassword);

            parameters.Add("Role", Messages.Role.USER);
            parameters.Add("IsActive", true);
            parameters.Add("IsDeleted", 0);

            _dapperService.Execute(StoredProcedures.RegisterUser, parameters);
        }

        /// <summary>
        /// Check if the email already exist in the DB for some other user or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolean value by verifying the email for the user</returns>
        public bool IsEmailExists(string email)
        {
            var param = new { Email = email };
            return _dapperService.ExecuteScalar<bool>(StoredProcedures.IsEmailExists, param, CommandType.StoredProcedure);
        }

    }
}
