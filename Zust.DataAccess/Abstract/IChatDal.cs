using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the Chat entity.
    /// </summary>
    public interface IChatDal : IEntityRepository<Chat>
    {
    }
}
