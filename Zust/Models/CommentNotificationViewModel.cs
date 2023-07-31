using Zust.Entities.Models;

namespace Zust.Web.Models
{
    public class CommentNotificationViewModel
    {
        public Comment? Comment { get; set; }
        public Notification? Notification { get; set; }
    }
}
