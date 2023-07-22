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

        public LikeService(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public async Task AddLikeToPostAsync(Like like)
        {
            await _likeDal.AddAsync(like);
        }

        public async Task<IEnumerable<string?>> GetPostIdsUserLikedAsync(string userId)
        {
            return (await _likeDal.GetAllAsync(l => l.UserId == userId)).Select(e => e.PostId);
        }

        public async Task<int> GetPostLikeCountAsync(string postId)
        {
            return (await _likeDal.GetAllAsync(l => l.PostId == postId)).Count();
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
    }
}
