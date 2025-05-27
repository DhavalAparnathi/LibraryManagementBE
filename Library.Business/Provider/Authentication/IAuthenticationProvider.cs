using Library.Business.ViewModel.Authentication;

namespace Library.Business.Provider.Authentication
{
    public interface IAuthenticationProvider
    {
        AuthResponseViewModel Login(AuthenticationViewModel model);

        //void Register(RegisterViewModel model);
    }
}
