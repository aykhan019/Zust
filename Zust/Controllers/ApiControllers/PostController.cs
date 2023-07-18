using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.Post)]
    [Controller]
    public class PostController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(IMediaService mediaService, IPostService postService, IUserService userService)
        {
            _mediaService = mediaService;
            _postService = postService;
            _userService = userService;
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
    }
}
