using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Comment entity that implements the IEntity interface.
    /// </summary>
    public class Comment : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the comment.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the post associated with the comment.
        /// </summary>
        public string? PostId { get; set; }

        /// <summary>
        /// Gets or sets the post associated with the comment.
        /// </summary>
        public virtual Post? Post { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who posted the comment.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who posted the comment.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Gets or sets the text content of the comment.
        /// </summary>
        public string? Text { get; set; }
    }
}
    