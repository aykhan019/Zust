using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Friendship entity that implements the IEntity interface.
    /// </summary>
    public class Friendship : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the friendship.
        /// </summary>
        public string? FriendshipId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the friendship.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the friend associated with the friendship.
        /// </summary>
        public string? FriendId { get; set; }

        /// <summary>
        /// Gets or sets the friend associated with the friendship.
        /// </summary>
        public virtual User? Friend { get; set; }
    }
}