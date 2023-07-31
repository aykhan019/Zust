using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;

namespace Zust.Web.Controllers.ApiControllers
{
    /// <summary>
    /// API controller responsible for handling post like-related operations.
    /// </summary>
    [Route(Routes.LikeController)]
    [ApiController]
    public class LikeController : ControllerBase
    {

        /// <summary>
        /// The service responsible for handling like-related operations.
        /// </summary>
        private readonly ILikeService _likeService;

        /// <summary>
        /// The service responsible for handling post-related operations.
        /// </summary>
        private readonly IPostService _postService;

        /// <summary>
        /// The service responsible for handling notification-related operations.
        /// </summary>
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeController"/> class with the specified services.
        /// </summary>
        /// <param name="likeService">The service for handling like-related operations.</param>
        /// <param name="postService">The service for handling post-related operations.</param>
        /// <param name="notificationService">The service for handling notification-related operations.</param>
        public LikeController(ILikeService likeService, IPostService postService, INotificationService notificationService)
        {
            _likeService = likeService;

            _postService = postService;

            _notificationService = notificationService;
        }

        /// <summary>
        /// Gets the number of likes for a post with the specified ID.
        /// </summary>
        /// <param name="postId">The ID of the post to get the like count for.</param>
        /// <returns>The number of likes for the post.</returns>
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

        /// <summary>
        /// Likes a post with the specified ID.
        /// </summary>
        /// <param name="postId">The ID of the post to be liked.</param>
        /// <returns>The updated number of likes for the post.</returns>
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

                var toUser = await _postService.GetOwnerOfPostByIdAsync(postId);

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

        /// <summary>
        /// Unlikes a post with the specified ID.
        /// </summary>
        /// <param name="postId">The ID of the post to be unliked.</param>
        /// <returns>The updated number of likes for the post.</returns>
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

        /// <summary>
        /// Gets the post IDs that the user has liked.
        /// </summary>
        /// <param name="userId">The ID of the user to get the liked post IDs for.</param>
        /// <returns>A list of post IDs that the user has liked.</returns>
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

        /// <summary>
        /// Checks if a user has liked a specific post.
        /// </summary>
        /// <param name="userId">The ID of the user to check if they have liked the post.</param>
        /// <param name="postId">The ID of the post to check if the user has liked.</param>
        /// <returns>True if the user has liked the post, false otherwise.</returns>
        [HttpGet(Routes.UserLikedPost)]
        public async Task<ActionResult<bool>> UserLikedPost(string userId, string postId)
        {
            try
            {
                var result = await _likeService.UserLikedPostAsync(userId, postId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
