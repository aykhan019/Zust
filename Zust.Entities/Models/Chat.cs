using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Chat entity that implements the IEntity interface.
    /// </summary>
    public class Chat : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the chat.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the sender user associated with the chat.
        /// </summary>
        public string? SenderUserId { get; set; }

        /// <summary>
        /// Gets or sets the sender user associated with the chat.
        /// </summary>
        public User? SenderUser { get; set; }

        /// <summary>
        /// Gets or sets the ID of the receiver user associated with the chat.
        /// </summary>
        public string? ReceiverUserId { get; set; }

        /// <summary>
        /// Gets or sets the receiver user associated with the chat.
        /// </summary>
        public User? ReceiverUser { get; set; }

        /// <summary>
        /// Gets or sets the collection of messages associated with the chat.
        /// </summary>
        public virtual IEnumerable<Message>? Messages { get; set; } = new List<Message>();
    }
}