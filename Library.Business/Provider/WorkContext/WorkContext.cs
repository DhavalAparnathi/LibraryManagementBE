using Library.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Library.Business.Provider.WorkContext
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the currently authenticated user's ID from the JWT claims.
        /// </summary>
        public int CurrentUserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null) throw new UnauthorizedAccessException(Messages.Authentication.InvalidAuthentication);

                return int.Parse(userIdClaim.Value);
            }
        }

        /// <summary>
        /// Gets the currently authenticated user's Role from the JWT claims.
        /// </summary>
        public string CurrentUserRole
        {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim == null) throw new UnauthorizedAccessException(Messages.Authentication.InvalidAuthentication);

                return roleClaim.Value;
            }
        }

    }
}
