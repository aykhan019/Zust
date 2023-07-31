using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    public class EFFriendshipDal : EfEntityRepositoryBase<Friendship, ZustDbContext>, IFriendshipDal
    {
    }
}
