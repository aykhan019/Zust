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
    public class LikeService : ILikeService
    {
        private readonly ILikeDal _likeDal;
        private readonly IUserService _userService;

        public LikeService(ILikeDal likeDal, IUserService userService)
        {
            _likeDal = likeDal;
            _userService = userService;
        }

        public async Task AddLikeToPostAsync(Like like)
        {
            await _likeDal.AddAsync(like);
        }

        public async Task DeleteLikeAsync(Like like)
        {
            await _likeDal.DeleteAsync(like);
        }

        public async Task DeleteUserLikesAsync(string userId)
        {
            var usersLikes = await _likeDal.GetAllAsync(l => l.UserId == userId);

            foreach (var userLike in usersLikes)
            {
                await RemoveLikeAsync(userLike);
            }
        }

        public async Task<IEnumerable<string?>> GetPostIdsUserLikedAsync(string userId)
        {
            return (await _likeDal.GetAllAsync(l => l.UserId == userId)).Select(e => e.PostId);
        }

        public async Task<int> GetPostLikeCountAsync(string postId)
        {
            return (await _likeDal.GetAllAsync(l => l.PostId == postId)).Count();
        }

        public async Task<IEnumerable<Like>> GetPostLikesAsync(string postId)
        {
            return await _likeDal.GetAllAsync(l => l.PostId == postId);
        }

        public async Task RemoveLikeAsync(Like like)
        {
            await _likeDal.DeleteAsync(like);
        }

        public async Task RemoveLikeAsync(string userId, string postId)
        {
            var like = await _likeDal.GetAsync(l => l.UserId == userId && l.PostId == postId);
            await RemoveLikeAsync(like);
        }

        public async Task<bool> UserLikedPostAsync(string userId, string postId)
        {
            var result = await _likeDal.GetAsync(l => l.UserId == userId && l.PostId == postId);

            return result != null;
        }
    }
}
