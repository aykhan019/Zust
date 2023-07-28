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
    public class PostService : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;

        public PostService(IPostDal postDal, IUserService userService, ILikeService likeService)
        {
            _postDal = postDal;
            _userService = userService;
            _likeService = likeService;
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

        public async Task<User?> GetOwnerOfPostById(string postId)
        {
            var post = await _postDal.GetAsync(p => p.Id == postId);

            var user = await _userService.GetUserByIdAsync(post.UserId);

            return user;
        }
    }
}

