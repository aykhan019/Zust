using Microsoft.AspNetCore.Identity;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class User : IdentityUser, IEntity
    {
        // Additional Properties
        public byte[]? PasswordSalt { get; set; }
    }
}
