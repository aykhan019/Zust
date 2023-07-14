using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class FriendRequest : IEntity
    {
        public string? Id { get; set; }
        public string? SenderId { get; set; }
        public virtual User? Sender { get; set; }
        public string? ReceiverId { get; set; }
        public virtual User? Receiver { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
    }
}
