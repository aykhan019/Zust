using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    /// <summary>
    /// Interface for managing comment-related operations, such as adding a new comment, retrieving comments of a post, and deleting comments.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds a new comment asynchronously.
        /// </summary>
        /// <param name="comment">The comment object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddAsync(Comment comment);

        /// <summary>
        /// Retrieves all comments of a post asynchronously based on the post ID.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A collection of comments of the post.</returns>
        Task<IEnumerable<Comment>> GetCommentsOfPostAsync(string postId);

        /// <summary>
        /// Deletes all comments of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose comments will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserCommentsAsync(string userId);

        /// <summary>
        /// Deletes a comment asynchronously.
        /// </summary>
        /// <param name="comment">The comment object to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteCommentAsync(Comment comment);
    }
}