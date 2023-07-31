namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a model for input data when adding a comment to a post.
    /// </summary>
    public class CommentInputModel
    {
        /// <summary>
        /// Gets or sets the ID of the post the comment belongs to.
        /// </summary>
        public string? PostId { get; set; }

        /// <summary>
        /// Gets or sets the text content of the comment.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who posted the comment.
        /// </summary>
        public string? UserId { get; set; }
    }
}
