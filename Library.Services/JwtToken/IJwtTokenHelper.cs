using Library.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.JwtToken
{
    public interface IJwtTokenHelper
    {
        /// <summary>
        /// Generates a JWT token for the specified user using application settings.
        /// </summary>
        string GenerateToken(Users user);
    }
}
