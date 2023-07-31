namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for updating user profile information.
    /// </summary>
    public class UpdateProfileViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the user whose profile is being updated.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the media file for the user's profile picture.
        /// </summary>
        public IFormFile? MediaFile { get; set; }
    }
}