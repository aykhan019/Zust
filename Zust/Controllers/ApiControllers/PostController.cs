using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.PostAPI)]
    [Controller]
    public class PostController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;
        private readonly IFriendshipService _friendshipService;

        public PostController(IMediaService mediaService, IPostService postService, IUserService userService, ICommentService commentService, INotificationService notificationService, IFriendshipService friendshipService)
        {
            _mediaService = mediaService;

            _postService = postService;

            _userService = userService;

            _commentService = commentService;

            _notificationService = notificationService;

            _friendshipService = friendshipService;
        }

        [HttpPost(Routes.CreatePost)]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostViewModel model)
        {
            try
            {
                IFormFile? mediaFile = model.MediaFile;

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var post = new Post()
                {
                    Id = Guid.NewGuid().ToString(),

                    CreatedAt = DateTime.Now,

                    Description = model.Description,

                    UserId = currentUser.Id
                };

                // If mediaFile is null, it mean post does not have an image
                if (mediaFile == null)
                {
                    post.ContentUrl = Constants.NoContentImageUrl;

                    post.HasMediaContent = false;

                    post.IsVideo = false;

                    await _postService.AddPostAsync(post);
                }
                else
                {
                    var mediaUrl = await _mediaService.UploadMediaAsync(mediaFile);

                    if (mediaUrl != string.Empty)
                    {
                        var isVideoFile = _mediaService.IsVideoFile(mediaFile);

                        post.ContentUrl = mediaUrl;

                        post.HasMediaContent = true;

                        post.IsVideo = isVideoFile;

                        await _postService.AddPostAsync(post);
                    }
                    else
                    {
                        return BadRequest(Errors.ImageUploadError);
                    }
                }
                post.User = await _userService.GetUserByIdAsync(post.UserId);

                // Send notification to all followers
                var followers = await _friendshipService.GetAllFollowersOfUserAsync(currentUser.Id);
                foreach (var follower in followers)
                {
                    var notification = new Notification()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Date = DateTime.Now,
                        FromUserId = currentUser.Id,
                        ToUserId = follower.Id,
                        IsRead = false,
                        Message = NotificationType.GetSharedPostMessage(currentUser.UserName)
                    };

                    await _notificationService.AddAsync(notification);
                }

                return Ok(post);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetAllPosts)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllPosts()
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var posts = await _postService.GetAllPostsForNewsFeedAsync(currentUser.Id);

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetAllPostsOfUser)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllPostsOfUser(string userId)
        {
            try
            {
                var posts = await _postService.GetAllPostsOfUserAsync(userId);

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetAllPostsLikeCount)]
        public async Task<ActionResult<int>> GetAllPostsLikeCount(string userId)
        {
            try
            {
                var postLikeCount = await _postService.GetAllPostsLikeCountAsync(userId);

                return Ok(postLikeCount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetCommentsOfPost)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsOfPost(string postId)
        {
            try
            {
                var comments = (await _commentService.GetCommentsOfPostAsync(postId)).ToList();

                comments.ForEach(async comment =>
                {
                    comment.User = await _userService.GetUserByIdAsync(comment.UserId);
                });

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetCountOfCommentsOfPost)]
        public async Task<ActionResult<int>> GetCountOfCommentsOfPost(string postId)
        {
            try
            {
                var comments = await _commentService.GetCommentsOfPostAsync(postId);

                return Ok(comments.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.AddComment)]
        public async Task<ActionResult<Comment>> AddComment([FromBody] CommentInputModel model)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(model.PostId);

                if (post == null)
                {
                    return NotFound();
                }
                var comment = new Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = model.Text,
                    UserId = model.UserId,
                    PostId = model.PostId
                };

                await _commentService.AddAsync(comment);


                var currentUser = await _userService.GetUserByIdAsync(model.UserId);

                var notification = new Notification()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    IsRead = false,
                    FromUserId = currentUser.Id,
                    FromUser = currentUser,
                    ToUserId = post.UserId,
                    ToUser = await _userService.GetUserByIdAsync(post.UserId),
                    Message = NotificationType.GetCommentedOnYourPostMessage(currentUser.UserName),
                };

                await _notificationService.AddAsync(notification);

                comment.User = await _userService.GetUserByIdAsync(model.UserId);

                var vm = new CommentNotificationViewModel()
                {
                    Notification = notification,
                    Comment = comment
                };
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
