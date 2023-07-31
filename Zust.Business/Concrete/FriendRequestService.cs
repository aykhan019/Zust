using System.Linq.Expressions;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestDal _friendRequestDal;

        public FriendRequestService(IFriendRequestDal friendRequestDal)
        {
            _friendRequestDal = friendRequestDal;
        }

        public async Task AddAsync(FriendRequest friendRequest)
        {
            await _friendRequestDal.AddAsync(friendRequest);
        }

        public async Task DeleteAsync(string id)
        {
            await _friendRequestDal.DeleteAsync(await GetAsync(f => f.Id == id));
        }

        public async Task DeleteAsync(FriendRequest friendRequest)
            {
            await _friendRequestDal.DeleteAsync(friendRequest);
        }

        public async Task UpdateAsync(FriendRequest friendRequest)
        {
            await _friendRequestDal.UpdateAsync(friendRequest);
        }

        public async Task<IEnumerable<FriendRequest>> GetAllAsync(Func<FriendRequest, bool>? filter = null)
        {
            var items = await _friendRequestDal.GetAllAsync();
            return filter == null ? items : items.Where(filter);
        }

        public async Task<bool> HasRequestPendingAsync(string senderId, string receiverId, string status)
        {
            var friendRequest = await _friendRequestDal.GetAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Status == status);

            return friendRequest != null;
        }

        public Task<FriendRequest?> GetAsync(Expression<Func<FriendRequest, bool>> filter)
        {
            return _friendRequestDal.GetAsync(filter);
        }

        public async Task<bool> CheckFriendRequestExistsAsync(string senderId, string receiverId, string status)
        {
            var friendRequest = await GetAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Status == status);
            return friendRequest != null;
        }

        public async Task DeleteUserFriendRequestsAsync(string userId)
        {
            var friendRequests = await _friendRequestDal.GetAllAsync(fr => fr.SenderId == userId || fr.ReceiverId == userId);

            foreach (var friendRequest in friendRequests)
            {
                await DeleteAsync(friendRequest);
            }
        }
    }
}
