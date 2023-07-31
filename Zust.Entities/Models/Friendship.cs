using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Friendship : IEntity
    {
        public string? FriendshipId { get; set; }

        public string? UserId { get; set; }

        public string? FriendId { get; set; }

        public virtual User? Friend { get; set; }
    }
}
