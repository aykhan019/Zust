using Zust.Core.Concrete;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    /// <summary>
    /// Represents a data access layer for the Notification entity.
    /// </summary>
    public interface INotificationDal : IEntityRepository<Notification>
    {
    }
}
