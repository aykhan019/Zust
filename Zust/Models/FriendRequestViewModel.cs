namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for handling friend requests.
    /// </summary>
    public class FriendRequestViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the friend request.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has a pending friend request.
        /// </summary>
        public bool HasFriendRequestPending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is already a friend.
        /// </summary>
        public bool IsFriend { get; set; }
    }
}
