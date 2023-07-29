using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface ILikeService
    {
        Task<int> GetPostLikeCountAsync(string postId);
        Task AddLikeToPostAsync(Like like);
        Task<IEnumerable<string>> GetPostIdsUserLikedAsync(string userId);
        Task RemoveLikeAsync(Like like);
        Task RemoveLikeAsync(string userId, string postId);
        Task<bool> UserLikedPostAsync(string userId, string postId);
        Task DeleteUserLikesAsync(string userId);
        Task<IEnumerable<Like>> GetPostLikesAsync(string postId);
        Task DeleteLikeAsync(Like like);
    }
}
