using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Chat : IEntity
    {
        public string? Id { get; set; }

        public string? SenderUserId { get; set; }
        
        public User? SenderUser { get; set;}

        public string? ReceiverUserId { get; set; }

        public User? ReceiverUser { get; set; }

        public virtual IEnumerable<Message>? Messages { get; set; } = new List<Message>();
    }
}
