using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Message : IEntity
    {
        public string? Id { get; set; }

        public DateTime DateSent { get; set; }

        public string? Text { get; set; }

        public string? ChatId { get; set; }

        public Chat? Chat { get; set; }

        public string? SenderUserId { get; set; }

        public User? SenderUser { get; set; }

        public string? ReceiverUserId { get; set; }

        public User? ReceiverUser { get; set; }
    }
}
