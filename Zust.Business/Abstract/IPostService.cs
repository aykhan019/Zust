using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IPostService
    {
        /// <summary>
        /// Adds a new post asynchronously.
        /// </summary>
        /// <param name="post">The post object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddPostAsync(Post post);

        /// <summary>
        /// Retrieves all posts asynchronously.
        /// </summary>
        /// <returns>A collection of Post objects representing all posts.</returns>
        Task<IEnumerable<Post>> GetAllPostsAsync();

        /// <summary>
        /// Retrieves all posts for the news feed of a user asynchronously based on the user ID.
        /// </summary>
        /// <param name="currentUserId">The ID of the user whose news feed will be retrieved.</param>
        /// <returns>A collection of Post objects representing the user's news feed posts.</returns>
        Task<IEnumerable<Post>> GetAllPostsForNewsFeedAsync(string currentUserId);

        /// <summary>
        /// Retrieves all posts of a user asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts will be retrieved.</param>
        /// <returns>A collection of Post objects representing the user's posts.</returns>
        Task<IEnumerable<Post>> GetAllPostsOfUserAsync(string userId);

        /// <summary>
        /// Retrieves a post by its ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to be retrieved.</param>
        /// <returns>The Post object representing the post.</returns>
        Task<Post> GetPostByIdAsync(string postId);

        /// <summary>
        /// Retrieves the total number of likes for all posts of a user asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The total number of likes for all posts of the user.</returns>
        Task<int> GetAllPostsLikeCountAsync(string userId);

        /// <summary>
        /// Retrieves the owner of a post by the post ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The User object representing the owner of the post.</returns>
        Task<User> GetOwnerOfPostByIdAsync(string postId);

        /// <summary>
        /// Deletes all posts of a user asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserPostsAsync(string userId);

        /// <summary>
        /// Deletes all comments of a user's posts asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose post comments will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserPostCommentsAsync(string userId);

        /// <summary>
        /// Deletes all likes of a user's posts asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose post likes will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserPostLikesAsync(string userId);
    }
}