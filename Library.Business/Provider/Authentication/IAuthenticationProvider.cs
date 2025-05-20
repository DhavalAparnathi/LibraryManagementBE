using Library.Business.ViewModel;
using Library.Business.ViewModel.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Provider.Authentication
{
    public interface IAuthenticationProvider
    {
        AuthResponseViewModel Login(AuthenticationViewModel model);

        void Register(RegisterViewModel model);
    }
}
