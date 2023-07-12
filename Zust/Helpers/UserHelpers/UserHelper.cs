using Microsoft.AspNetCore.Identity;
using Zust.Entities.Models;

namespace Zust.Web.Helpers.UserHelpers
{
    public class UserHelper
    {
        public static async Task<User?> GetCurrentUserAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userManager = httpContext.RequestServices.GetService<UserManager<User>>();
                return await userManager.GetUserAsync(httpContext.User);
            }
            return null!;
        }
    }
}
