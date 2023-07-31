using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    public class EFUserDal : EfEntityRepositoryBase<User, ZustDbContext>, IUserDal
    {
    }
}
