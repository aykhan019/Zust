using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Notification : IEntity
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime Date { get; set; }

        // For EFNotificationDal
        public Notification()
        {

        }

        public Notification(string message, string userId)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Message = message;
            Date = DateTime.Now;
            IsRead = false;
        }
    }
}
