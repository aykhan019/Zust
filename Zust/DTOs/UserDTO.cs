namespace Zust.Web.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for user-related information.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's profile image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's cover image.
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a friend of the current user.
        /// </summary>
        public bool IsFriend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has a pending friend request from the current user.
        /// </summary>
        public bool HasFriendRequestPending { get; set; }
    }
}
