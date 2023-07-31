using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Message entity that implements the IEntity interface.
    /// </summary>
    public class Message : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the message.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the date when the message was sent.
        /// </summary>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or sets the text content of the message.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the ID of the chat associated with the message.
        /// </summary>
        public string? ChatId { get; set; }

        /// <summary>
        /// Gets or sets the chat associated with the message.
        /// </summary>
        public Chat? Chat { get; set; }

        /// <summary>
        /// Gets or sets the ID of the sender user associated with the message.
        /// </summary>
        public string? SenderUserId { get; set; }

        /// <summary>
        /// Gets or sets the sender user associated with the message.
        /// </summary>
        public User? SenderUser { get; set; }

        /// <summary>
        /// Gets or sets the ID of the receiver user associated with the message.
        /// </summary>
        public string? ReceiverUserId { get; set; }

        /// <summary>
        /// Gets or sets the receiver user associated with the message.
        /// </summary>
        public User? ReceiverUser { get; set; }
    }
}