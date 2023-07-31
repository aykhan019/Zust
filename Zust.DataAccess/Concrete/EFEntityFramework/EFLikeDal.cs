using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the ILikeDal interface.
    /// </summary>
    public class EFLikeDal : EfEntityRepositoryBase<Like, ZustDbContext>, ILikeDal
    {
    }
}
