using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IFriendRequestService
    {
        Task AddAsync(FriendRequest friendRequest);
        Task<IEnumerable<FriendRequest>> GetAllAsync(Func<FriendRequest, bool>? filter = null);
        Task<FriendRequest?> GetAsync(Expression<Func<FriendRequest, bool>  > filter);
        Task DeleteAsync(string id);
        Task DeleteAsync(FriendRequest friendRequest);
        Task UpdateAsync(FriendRequest friendRequest);
    }
}
