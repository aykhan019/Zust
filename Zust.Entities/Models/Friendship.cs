using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entities.Models
{
    public class Friendship
    {
        public int FriendshipId { get; set; }

        public string? UserId { get; set; }

        public string? FriendId { get; set; }

        public virtual User? Friend { get; set; }
    }
}
