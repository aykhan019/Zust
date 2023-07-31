using Zust.Web.Entities;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for the index page.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the view model for creating a post.
        /// </summary>
        public CreatePostViewModel? CreatePostViewModel { get; set; }

        /// <summary>
        /// Gets or sets a list of videos to watch.
        /// </summary>
        public List<Video>? WatchVideos { get; set; }

        /// <summary>
        /// Gets or sets a list of advertisements.
        /// </summary>
        public List<Advertisement>? Advertisements { get; set; }
    }
}
