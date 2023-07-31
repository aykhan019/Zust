namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for messages.
    /// </summary>
    public class MessageViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the sender user.
        /// </summary>
        public string? SenderUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the receiver user.
        /// </summary>
        public string? ReceiverUserId { get; set; }

        /// <summary>
        /// Gets or sets the text content of the message.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the ID of the chat to which the message belongs.
        /// </summary>
        public string? ChatId { get; set; }
    }
}
