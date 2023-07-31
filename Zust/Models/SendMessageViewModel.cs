using Zust.Entities.Models;

namespace Zust.Web.Models
{
    public class SendMessageViewModel
    {
        public Message Message { get; set; }
        public bool FirstMessageSent { get; set; }
    }
}
