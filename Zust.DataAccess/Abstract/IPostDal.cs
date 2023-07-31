using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the Post entity.
    /// </summary>
    public interface IPostDal : IEntityRepository<Post>
    {
    }
}
