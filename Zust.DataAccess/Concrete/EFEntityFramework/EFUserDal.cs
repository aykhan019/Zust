using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the IUserDal interface.
    /// </summary>
    public class EFUserDal : EfEntityRepositoryBase<User, ZustDbContext>, IUserDal
    {
    }
}
