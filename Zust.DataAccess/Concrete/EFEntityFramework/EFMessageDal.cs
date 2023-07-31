using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{
    /// <summary>
    /// Represents the Entity Framework implementation of the IMessageDal interface.
    /// </summary>
    public class EFMessageDal : EfEntityRepositoryBase<Message, ZustDbContext>, IMessageDal
    {
    }
}
