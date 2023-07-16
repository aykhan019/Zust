using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipDal _friendshipDal;
        private readonly IUserService _userService;

        public FriendshipService(IFriendshipDal friendshipDal, IUserService userService)
        {
            _friendshipDal = friendshipDal;
            _userService = userService;
        }

        public async Task AddFriendship(Friendship friendship)
        {
            await _friendshipDal.AddAsync(friendship);
        }

        public async Task<IEnumerable<User?>> GetAllFollowersOfUserAsync    (string userId)
        {
            var friendships = await _friendshipDal.GetAllAsync();

            return friendships.Where(f => f.FriendId == userId)
                              .Select(async u =>
                              {
                                  return await _userService.GetUserByIdAsync(u.UserId);
                              })
                              .Select(task => task.Result);
        }

        public async Task<IEnumerable<User?>> GetAllFollowingsOfUserAsync(string userId)
        {
            var friendships = await _friendshipDal.GetAllAsync();

            return friendships.Where(f => f.UserId == userId)
                               .Select(async u =>
                               {
                                   return await _userService.GetUserByIdAsync(u.FriendId);
                               })
                               .Select(task => task.Result);
        }

        public async Task<Friendship?> GetFriendshipAsync(string userId, string friendId)
        {
            var friendship = await _friendshipDal.GetAsync(f => f.UserId == userId && f.FriendId == friendId);

            return friendship;
        }

        public async Task<bool> DeleteFriendshipAsync(string userId, string friendId)
        {
            var friendship = await GetFriendshipAsync(userId, friendId);
            if (friendId != null)
            {
                await _friendshipDal.DeleteAsync(friendship);
                return true;
            }
            return false;
        }
    }
}
