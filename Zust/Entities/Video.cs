namespace Zust.Web.Entities
{
    /// <summary>
    /// Represents a video entity with its properties.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Gets or sets the URL of the video.
        /// </summary>
        public string? VideoUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the video's thumbnail or image.
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}