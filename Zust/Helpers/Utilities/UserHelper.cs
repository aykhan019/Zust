using Microsoft.AspNetCore.Identity;
using Zust.Entities.Models;

namespace Zust.Web.Helpers.Utilities
{
    /// <summary>
    /// Helper class for retrieving the current authenticated user.
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// Retrieves the current authenticated user from the HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext representing the current HTTP request.</param>
        /// <returns>The current authenticated user, or null if not authenticated.</returns>
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