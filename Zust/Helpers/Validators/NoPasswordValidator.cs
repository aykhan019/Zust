using Microsoft.AspNetCore.Identity;

namespace Zust.Web.Helpers.Validators
{
    // Create a custom password validator that does not enforce any constraints
    public class NoPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            // Return a success result without performing any validation
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
