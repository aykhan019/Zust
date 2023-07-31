using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the INotificationDal interface.
    /// </summary>
    public class EFNotificationDal : EfEntityRepositoryBase<Notification, ZustDbContext>, INotificationDal
    {
    }
}
