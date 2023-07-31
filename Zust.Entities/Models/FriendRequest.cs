using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a FriendRequest entity that implements the IEntity interface.
    /// </summary>
    public class FriendRequest : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the friend request.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the sender user associated with the friend request.
        /// </summary>
        public string? SenderId { get; set; }

        /// <summary>
        /// Gets or sets the sender user associated with the friend request.
        /// </summary>
        public virtual User? Sender { get; set; }

        /// <summary>
        /// Gets or sets the ID of the receiver user associated with the friend request.
        /// </summary>
        public string? ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the receiver user associated with the friend request.
        /// </summary>
        public virtual User? Receiver { get; set; }

        /// <summary>
        /// Gets or sets the date of the friend request.
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the friend request.
        /// </summary>
        public string? Status { get; set; }
    }
}