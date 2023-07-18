using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.UserHelpers;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.Post)]
    [Controller]
    public class PostController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IPostService _postService;

        public PostController(IMediaService mediaService, IPostService postService)
        {
            _mediaService = mediaService;
            _postService = postService;
        }

        [HttpPost(Routes.CreatePost)]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostViewModel model)
        {
            try
            {
                IFormFile? mediaFile = model.MediaFile;
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);
                // If mediaFile is null, it mean post does not have an image
                if (mediaFile == null)
                {
                    var post = new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.Now,
                        ContentUrl = Constants.NoContentImageUrl,
                        Description = model.Description,
                        HasMediaContent = false,
                        IsVideo = false,
                        UserId = currentUser.Id
                    };
                    await _postService.AddPostAsync(post);
                }
                else
                {
                    var mediaUrl = await _mediaService.UploadMediaAsync(mediaFile);
                    if (mediaUrl != string.Empty)
                    {
                        var isVideoFile = _mediaService.IsVideoFile(mediaFile);

                        var post = new Post()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ContentUrl = mediaUrl,
                            CreatedAt = DateTime.Now,
                            Description = model.Description,
                            HasMediaContent = true,
                            IsVideo = isVideoFile,
                            UserId = currentUser.Id
                        };
                        await _postService.AddPostAsync(post);
                    }
                    else
                    {
                        return BadRequest(Errors.ImageUploadError);
                    }

                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
