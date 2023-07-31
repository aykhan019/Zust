using Zust.Entities.Models;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for displaying a comment and its associated notification.
    /// </summary>
    public class CommentNotificationViewModel
    {
        /// <summary>
        /// Gets or sets the comment to be displayed.
        /// </summary>
        public Comment? Comment { get; set; }

        /// <summary>
        /// Gets or sets the notification associated with the comment.
        /// </summary>
        public Notification? Notification { get; set; }
    }
}
