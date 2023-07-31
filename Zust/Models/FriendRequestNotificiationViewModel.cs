using Zust.Entities.Models;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for handling friend request notifications.
    /// </summary>
    public class FriendRequestNotificiationViewModel
    {
        /// <summary>
        /// Gets or sets the friend request associated with the notification.
        /// </summary>
        public FriendRequest? FriendRequest { get; set; }

        /// <summary>
        /// Gets or sets the notification related to the friend request.
        /// </summary>
        public Notification? Notification { get; set; }
    }
}
