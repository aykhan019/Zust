using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the ICommentDal interface.
    /// </summary>
    public class EFCommentDal : EfEntityRepositoryBase<Comment, ZustDbContext>, ICommentDal
    {
    }
}
