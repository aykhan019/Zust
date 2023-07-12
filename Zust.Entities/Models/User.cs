using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection.Metadata;
using Zust.Core.Abstraction;
using Zust.Entities.Helpers;

namespace Zust.Entities.Models
{
    public class User : IdentityUser, IEntity
    {
        public string? ImageUrl { get; set; } = Constants.DefaultProfileImagePath;

        public string? CoverImage { get; set; } 

        public string? Birthday { get; set; }

        public string? Occupation { get; set; }

        public string? Birthplace { get; set; }

        public string? Gender { get; set; }

        public string? RelationshipStatus { get; set; }

        public string? BloodGroup { get; set; }

        public string? Website { get; set; }

        public string? SocialLink { get; set; }

        public string? Languages { get; set; }

        public string? AboutMe { get; set; }

        public string? EducationWork { get; set; }

        public string? Interests { get; set; }

        public virtual ICollection<Friendship>? Friendships { get; set; }
    }
}
