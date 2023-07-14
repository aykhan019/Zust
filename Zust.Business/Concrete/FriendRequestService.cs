using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public Task<FriendRequest?> GetAsync(Expression<Func<FriendRequest, bool>> filter)
        {
            return _friendRequestDal.GetAsync(filter);
        }
    }
}
