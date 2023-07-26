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

        public async Task<IEnumerable<Comment>> GetCommentsOfPostAsync(string postId)
        {
            return await _commentDal.GetAllAsync(c => c.PostId == postId);
        }
    }
}
