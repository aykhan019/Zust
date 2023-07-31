using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Notification entity that implements the IEntity interface.
    /// </summary>
    public class Notification : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the notification.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who sent the notification.
        /// </summary>
        public string? FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the user who sent the notification.
        /// </summary>
        public virtual User? FromUser { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who will receive the notification.
        /// </summary>
        public string? ToUserId { get; set; }

        /// <summary>
        /// Gets or sets the user who will receive the notification.
        /// </summary>
        public virtual User? ToUser { get; set; }

        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notification has been read or not.
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Gets or sets the date when the notification was sent.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Initializes a new instance of the Notification class.
        /// </summary>
        /// For EFNotificationDal
        public Notification()
        {

        }
    }
}
