using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
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

        private readonly IPostService _postService;

        private readonly INotificationService _notificationService;

        public LikeController(ILikeService likeService, IPostService postService, INotificationService notificationService)
        {
            _likeService = likeService;

            _postService = postService;

            _notificationService = notificationService;
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

                var toUser = await _postService.GetOwnerOfPostById(postId);

                var notification = new Notification()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    IsRead = false,
                    FromUserId = currentUser.Id,
                    ToUser = toUser,
                    ToUserId = toUser.Id,
                    Message = NotificationType.GetLikedYourPostMessage(currentUser.UserName),
                };

                await _notificationService.AddAsync(notification);

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

        [HttpGet(Routes.UserLikedPost)]
        public async Task<ActionResult<bool>> UserLikedPost(string userId, string postId)
        {
            try
            {
                var result = await _likeService.UserLikedPost(userId, postId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
