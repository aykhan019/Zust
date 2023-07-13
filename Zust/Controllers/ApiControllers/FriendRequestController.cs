using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.UserHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    /// <summary>
    /// Controller responsible for handling friend request-related actions.
    /// </summary>
    [Route(Routes.FriendRequest)]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        /// <summary>
        /// Gets or sets the friend request service used for friend request-related operations.
        /// </summary>
        private readonly IFriendRequestService _friendRequestService;

        /// <summary>
        /// Initializes a new instance of the FriendRequestController class with the required dependencies.
        /// </summary>
        /// <param name="friendRequestService">The service for friend request-related operations.</param>
        public FriendRequestController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

        /// <summary>
        /// Adds a new friend request from the current user to the specified receiver.
        /// </summary>
        /// <param name="receiverId">The ID of the receiver user.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost(Routes.AddFriendRequest)]
        public async Task<IActionResult> AddFriendRequest(string receiverId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);
                var friendRequest = new FriendRequest()
                {
                    Id = Guid.NewGuid().ToString(),
                    SenderId = currentUser.Id,
                    ReceiverId = receiverId,
                    RequestDate = DateTime.Now,
                    Status = Status.Pending  
                };  
                await _friendRequestService.AddAsync(friendRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancels a friend request sent by the current user to the specified receiver.
        /// </summary>
        /// <param name="receiverId">The ID of the receiver user.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost(Routes.CancelFriendRequest)]
        public async Task<IActionResult> CancelFriendRequest(string receiverId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);
                var friendRequest = await _friendRequestService.GetAsync(f => f.ReceiverId == receiverId && f.SenderId == currentUser.Id);
                if (friendRequest != null)
                {
                    await _friendRequestService.DeleteAsync(friendRequest);
                    return Ok();
                }
                else
                {
                    return BadRequest(Errors.FriendRequestNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the list of friend requests sent by the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of FriendRequest objects.</returns>
        [HttpGet(Routes.GetSentFriendRequests)]
        public async Task<IEnumerable<FriendRequest>> GetSentFriendRequests(string userId)
        {
            var friendRequests = await _friendRequestService.GetAllAsync(f => f.SenderId == userId);
            return friendRequests;
        }
    }
}
