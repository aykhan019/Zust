using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the FriendRequest entity.
    /// </summary>
    public interface IFriendRequestDal : IEntityRepository<FriendRequest>
    {
    }
}
