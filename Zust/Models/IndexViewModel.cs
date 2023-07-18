using Zust.Web.Entities;

namespace Zust.Web.Models
{
    public class IndexViewModel
    {
        public CreatePostViewModel? CreatePostViewModel { get; set; }
        public List<Video>? WatchVideos { get; set; }
        public List<Advertisement>? Advertisements { get; set; }
    }
}
