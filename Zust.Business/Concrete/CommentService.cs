using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentService(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task AddAsync(Comment comment)
        {
            await _commentDal.AddAsync(comment);    
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            await _commentDal.DeleteAsync(comment);
        }

        public async Task DeleteUserCommentsAsync(string userId)
        {
            var comments = await _commentDal.GetAllAsync(c => c.UserId == userId);

            foreach (var comment in comments)
            {
                await _commentDal.DeleteAsync(comment);
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfPostAsync(string postId)
        {
            return await _commentDal.GetAllAsync(c => c.PostId == postId);
        }
    }
}
