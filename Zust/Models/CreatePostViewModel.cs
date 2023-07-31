namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for creating a new post.
    /// </summary>
    public class CreatePostViewModel
    {
        /// <summary>
        /// Gets or sets the description of the post.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the media file associated with the post.
        /// </summary>
        public IFormFile? MediaFile { get; set; }
    }
}
