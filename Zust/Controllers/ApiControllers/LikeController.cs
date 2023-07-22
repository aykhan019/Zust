using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.LikeController)]
    [ApiController]
    public class LikeController : ControllerBase
    {

        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet(Routes.GetPostLikeCount)]
        public async Task<ActionResult<int?>> GetPostLikeCount(string postId)
        {
            try
            {
                var count = await _likeService.GetPostLikeCountAsync(postId);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.LikePost)]
        public async Task<ActionResult<int?>> LikePost(string postId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var like = new Like()
                {
                    Id = Guid.NewGuid().ToString(),
                    PostId = postId,
                    UserId = currentUser.Id
                };

                await _likeService.AddLikeToPostAsync(like);

                var likeCount = await _likeService.GetPostLikeCountAsync(postId);

                return Ok(likeCount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.UnlikePost)]
        public async Task<ActionResult<int?>> UnlikePost(string postId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                await _likeService.RemoveLikeAsync(currentUser.Id, postId);

                var likeCount = await _likeService.GetPostLikeCountAsync(postId);

                return Ok(likeCount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetPostIdsLiked)]
        public async Task<ActionResult<IEnumerable<string>>> GetPostsIdsLikeByUser(string userId)
        {
            try
            {
                var ids = await _likeService.GetPostIdsUserLikedAsync(userId);

                return Ok(ids);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
