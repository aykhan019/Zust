using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the Comment entity.
    /// </summary>
    public interface ICommentDal : IEntityRepository<Comment>
    {
    }
}   
