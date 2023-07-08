using Microsoft.AspNetCore.Identity;
using System;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class User : IdentityUser, IEntity
    {
        public string? ImageUrl { get; set; } = string.Empty;

        public virtual ICollection<Friendship> Friendships { get; set; }
    }
}
