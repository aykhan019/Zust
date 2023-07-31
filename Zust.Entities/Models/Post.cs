using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    /// <summary>
    /// Represents a Post entity that implements the IEntity interface.
    /// </summary>
    public class Post : IEntity
    {
        /// <summary>
        /// Gets or sets the ID of the post.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the post.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the post has media content or not.
        /// </summary>
        public bool HasMediaContent { get; set; }

        /// <summary>
        /// Gets or sets the URL of the media content (e.g., image or video) associated with the post.
        /// </summary>
        public string? ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the post contains a video or not.
        /// </summary>
        public bool IsVideo { get; set; }

        /// <summary>
        /// Gets or sets the date when the post was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the post.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who created the post.
        /// </summary>
        public virtual User? User { get; set; }
    }
}