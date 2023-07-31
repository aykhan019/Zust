using System.Linq.Expressions;
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
        Task<bool> CheckFriendRequestExistsAsync(string senderId, string receiverId, string status);
        Task<bool> HasRequestPendingAsync(string senderId, string receiverId, string status);
        Task DeleteUserFriendRequestsAsync(string userId);
    }
}
