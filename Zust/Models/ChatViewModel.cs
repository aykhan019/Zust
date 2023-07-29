    using Zust.Entities.Models;

namespace Zust.Web.Models
{
    public class ChatViewModel
    {
        public User? UserToChat { get; set; }

        public User? CurrentUser { get; set; }

        public Chat? Chat { get; set; }

        public Chat? ChatForOtherUser { get; set; }
    }
}
