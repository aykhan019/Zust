using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the IPostDal interface.
    /// </summary>
    public class EFPostDal : EfEntityRepositoryBase<Post, ZustDbContext>, IPostDal
    {
    }
}
