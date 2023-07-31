using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Notification : IEntity
    {
        public string? Id { get; set; }

        public string? FromUserId { get; set; }

        public virtual User? FromUser { get; set; }

        public string? ToUserId { get; set; }

        public virtual User? ToUser { get; set; }

        public string? Message { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime Date { get; set; }

        // For EFNotificationDal
        public Notification()
        {

        }
    }
}
