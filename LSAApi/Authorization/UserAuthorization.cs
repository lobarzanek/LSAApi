using LSAApi.Models;
using System.Security.Claims;

namespace LSAApi.Authorization
{
    public class UserAuthorization
    {
        public User? GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return null;
            }
            var userClaims = identity.Claims;

            var user = new User
            {
                UserId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
                UserName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                UserLogin = userClaims.FirstOrDefault(c => c.Type == "UserLogin")?.Value,
                RoleId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value),
            };
            return user;
        }
    }
}
