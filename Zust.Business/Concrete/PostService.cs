using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;

        public PostService(IPostDal postDal, IUserService userService, ILikeService likeService, ICommentService commentService)
        {
            _postDal = postDal;
            _userService = userService;
            _likeService = likeService;
            _commentService = commentService;
        }

        public async Task AddPostAsync(Post post)
        {
            await _postDal.AddAsync(post);
        }

        public async Task<IEnumerable<Post>> GetAllPostsForNewsFeedAsync(string currentUserId)
        {
            var posts = (await GetAllPostsAsync()).Where(p => p.UserId != currentUserId);

            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = (await _postDal.GetAllAsync()).ToList();

            posts.ForEach(async p => p.User = await _userService.GetUserByIdAsync(p.UserId));

            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllPostsOfUserAsync(string userId)
        {
            var allPosts = (await GetAllPostsAsync()).Where(p => p.UserId == userId);

            return allPosts;
        }

        public Task<Post?> GetPostByIdAsync(string postId)
        {
            return _postDal.GetAsync(p => p.Id == postId);
        }

        public async Task<int> GetAllPostsLikeCountAsync(string userId)
        {
            var posts = await GetAllPostsOfUserAsync(userId);

            var tasks = posts.Select(p => _likeService.GetPostLikeCountAsync(p.Id));

            int[] likeCounts = await Task.WhenAll(tasks);

            int totalLikesCount = likeCounts.Sum();

            return totalLikesCount;
        }

        public async Task<User?> GetOwnerOfPostByIdAsync(string postId)
        {
            var post = await _postDal.GetAsync(p => p.Id == postId);

            var user = await _userService.GetUserByIdAsync(post.UserId);

            return user;
        }

        public async Task DeleteUserPostsAsync(string userId)
        {
            var posts = await _postDal.GetAllAsync(p => p.UserId == userId);

            foreach (var post in posts)
            {
                await _postDal.DeleteAsync(post);
            }
        }

        public async Task DeleteUserPostCommentsAsync(string userId)
        {
            var posts = await _postDal.GetAllAsync(p => p.UserId == userId);

            foreach (var post in posts)
            {
                var comments = await _commentService.GetCommentsOfPostAsync(post.Id);

                foreach (var comment in comments)
                {
                    await _commentService.DeleteCommentAsync(comment);
                }
            }
        }

        public async Task DeleteUserPostLikesAsync(string userId)
        {
            var posts = await _postDal.GetAllAsync(p => p.UserId == userId);

            foreach (var post in posts)
            {
                var likes = await _likeService.GetPostLikesAsync(post.Id);

                foreach (var like in likes)
                {
                    await _likeService.DeleteLikeAsync(like);
                }
            }
        }
    }
}

