using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IPostService
    {
        Task AddPostAsync(Post post);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetAllPostsForNewsFeedAsync(string currentUserId);
        Task<IEnumerable<Post>> GetAllPostsOfUserAsync(string userId);
        Task<Post> GetPostByIdAsync(string postId);
        Task<int> GetAllPostsLikeCountAsync(string userId);
        Task<User> GetOwnerOfPostById(string postId);
    }
}
