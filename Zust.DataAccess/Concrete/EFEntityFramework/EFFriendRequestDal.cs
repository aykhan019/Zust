using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    public class EFFriendRequestDal : EfEntityRepositoryBase<FriendRequest, ZustDbContext>, IFriendRequestDal
    {
    }
}
