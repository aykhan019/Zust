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
        Task<User> GetOwnerOfPostByIdAsync(string postId);
        Task DeleteUserPostsAsync(string userId);
        Task DeleteUserPostCommentsAsync(string userId);
        Task DeleteUserPostLikesAsync(string userId);
    }
}
