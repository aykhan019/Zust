using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a User entity that inherits from IdentityUser and implements the IEntity interface.
    /// </summary>
    public class User : IdentityUser, IEntity
    {
        /// <summary>
        /// Gets or sets the URL of the user's profile image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's cover image.
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the occupation of the user.
        /// </summary>
        public string? Occupation { get; set; }

        /// <summary>
        /// Gets or sets the birthplace of the user.
        /// </summary>
        public string? Birthplace { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the relationship status of the user.
        /// </summary>
        public string? RelationshipStatus { get; set; }

        /// <summary>
        /// Gets or sets the blood group of the user.
        /// </summary>
        public string? BloodGroup { get; set; }

        /// <summary>
        /// Gets or sets the website URL of the user.
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Gets or sets the social link of the user.
        /// </summary>
        public string? SocialLink { get; set; }

        /// <summary>
        /// Gets or sets the languages known by the user.
        /// </summary>
        public string? Languages { get; set; }

        /// <summary>
        /// Gets or sets a brief description about the user.
        /// </summary>
        public string? AboutMe { get; set; }

        /// <summary>
        /// Gets or sets the user's education and work details.
        /// </summary>
        public string? EducationWork { get; set; }

        /// <summary>
        /// Gets or sets the user's interests and hobbies.
        /// </summary>
        public string? Interests { get; set; }
    }
}
