using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    /// <summary>
    /// Represents the CommentService that implements the ICommentService interface.
    /// This service provides functionalities related to managing comments.
    /// </summary>
    public class CommentService : ICommentService
    {
        /// <summary>
        /// Private field for accessing the data access layer that handles comment-related operations.
        /// </summary>
        private readonly ICommentDal _commentDal;

        /// <summary>
        /// Initializes a new instance of the CommentService class with the specified CommentDal dependency.
        /// </summary>
        /// <param name="commentDal">The data access layer for comments.</param>
        public CommentService(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        /// <summary>
        /// Adds a new comment asynchronously.
        /// </summary>
        /// <param name="comment">The Comment object representing the new comment to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddAsync(Comment comment)
        {
            await _commentDal.AddAsync(comment);
        }

        /// <summary>
        /// Deletes a comment asynchronously.
        /// </summary>
        /// <param name="comment">The Comment object representing the comment to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteCommentAsync(Comment comment)
        {
            await _commentDal.DeleteAsync(comment);
        }

        /// <summary>
        /// Deletes all comments of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose comments are to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteUserCommentsAsync(string userId)
        {
            var comments = await _commentDal.GetAllAsync(c => c.UserId == userId);

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    await _commentDal.DeleteAsync(comment);
                }
            }
        }

        /// <summary>
        /// Retrieves all comments of a post asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post whose comments are to be retrieved.</param>
        /// <returns>A collection of Comment objects representing all comments of the post.</returns>
        public async Task<IEnumerable<Comment>> GetCommentsOfPostAsync(string postId)
        {
            return await _commentDal.GetAllAsync(c => c.PostId == postId);
        }
    }
}
