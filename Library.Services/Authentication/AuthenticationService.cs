using Dapper;
using Library.Data.Repository;
using Library.Models.Users;
using Library.Utilities.Constants;
using Microsoft.AspNetCore.Identity;

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

            var user = _dapperService.QueryFirstOrDefault<Users>(StoredProcedures.ValidateUser, param);
            if (user == null || user.IsDeleted)
                return null;

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(null, user.PasswordHash, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        /// <summary>
        /// Method that resets user password based on confirmation of old password & new password.
        /// </summary>
        /// <returns>Resets the old password.</returns>
        public void ResetPassword(int userId, string oldPassword, string newPassword)
        {
            var user = _dapperService.QueryFirstOrDefault<Users>(StoredProcedures.GetUserById, new { Id = userId });

            if (user == null || user.IsDeleted)
                throw new Exception(Messages.User.UserNotFound);

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(null, user.PasswordHash, oldPassword);

            if (result != PasswordVerificationResult.Success)
                throw new Exception(Messages.Authentication.InvalidOldPassword);

            var newHashedPassword = hasher.HashPassword(null, newPassword);
            var parameters = new DynamicParameters();
            parameters.Add("Id", userId);
            parameters.Add("PasswordHash", newHashedPassword);

            _dapperService.Execute(StoredProcedures.ResetUserPassword, parameters);
        }
    }
}
