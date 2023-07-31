using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the IFriendshipDal interface.
    /// </summary>
    public class EFFriendshipDal : EfEntityRepositoryBase<Friendship, ZustDbContext>, IFriendshipDal
    {
    }
}
