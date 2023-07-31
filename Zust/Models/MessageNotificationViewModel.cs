using Zust.Entities.Models;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for message notifications.
    /// </summary>
    public class MessageNotificationViewModel
    {
        /// <summary>
        /// Gets or sets the message associated with the notification.
        /// </summary>
        public Message? Message { get; set; }

        /// <summary>
        /// Gets or sets the notification related to the message.
        /// </summary>
        public Notification? Notification { get; set; }
    }
}
