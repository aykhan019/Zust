using Zust.Entities.Models;
using Zust.Web.Migrations;

namespace Zust.Web.Models
{
    public class MessageNotificationViewModel
    {
        public Message? Message { get; set; }
        public Notification? Notification { get; set; }
    }
}
