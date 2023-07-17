using Zust.Entities.Models;

namespace Zust.Web.Models
{
    public class CreatePostViewModel
    {
        public string? Description { get; set; }
        public IFormFile? MediaFile { get; set; }
    }
}
