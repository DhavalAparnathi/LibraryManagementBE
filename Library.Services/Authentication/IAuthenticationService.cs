using Library.Business.ViewModel;
using Library.Models.Users;

namespace Library.Services.Authentication
{
    public interface IAuthenticationService
    {
        Users ValidateUser(string email, string password);

        void ResetPassword(int userId, string oldPassword, string newPassword);
    }
}
