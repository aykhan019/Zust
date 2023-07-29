namespace Zust.Web.Models
{
    public class MessageViewModel
    {
        public string? SenderUserId { get; set; }

        public string? ReceiverUserId { get; set; }

        public string? Text { get; set; }

        public string? ChatId { get; set; }
    }
}
