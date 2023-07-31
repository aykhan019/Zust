using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the User entity.
    /// </summary>
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
