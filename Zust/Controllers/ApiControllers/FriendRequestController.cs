using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Zust.Business.Abstract;
using Zust.Core.Concrete.EntityFramework;
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
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the FriendRequestController class with the required dependencies.
        /// </summary>
        /// <param name="friendRequestService">The service for friend request-related operations.</param>
        public FriendRequestController(IFriendRequestService friendRequestService, IUserService userService)
        {
            _friendRequestService = friendRequestService;
            _userService = userService;
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
        public async Task<IActionResult> GetSentFriendRequests(string userId)
        {
            try
            {
                var friendRequests = await _friendRequestService.GetAllAsync(f => f.SenderId == userId);
                return Ok(friendRequests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the list of friend requests recevied by the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of FriendRequest objects.</returns>
        [HttpGet(Routes.GetReceivedFriendRequests)]
        public async Task<IActionResult> GetReceivedFriendRequests(string userId)
        {
            try
            {
                


                var friendRequests = await _friendRequestService.GetAllAsync(f => f.ReceiverId == userId);
                var friendRequestsList = friendRequests.Where(fr => fr.Status != Status.Accepted).ToList();

                foreach (var fr in friendRequestsList)
                {
                    fr.Sender = await _userService.GetUserById(fr.SenderId);
                    fr.Receiver = await _userService.GetUserById(fr.ReceiverId);
                }

                return Ok(friendRequestsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.AcceptRequest)]
        public IActionResult AcceptRequest(string requestId)
        {
            //var friendRequest = await _friendRequestService.GetAsync(fr => fr.Id == requestId);
            //if (friendRequest != null)
            //{
            //    var friendShip = new Friendship()
            //    {
            //         FriendshipId = Guid.NewGuid().ToString(),
            //          FriendId = friendRequest.SenderId,
            //           UserId = friendRequest.ReceiverId,
            //    };
            //    _userService.AddFriendToUser(_userService.GetUserById(friendRequest.SenderId), friendRequest);
            //    AddFriendToUser(friendRequest.Sender, friendRequest.Receiver);
            //    // Other necessary operations
            //    return Ok(); // Return an appropriate response
            //}
            //else
            //{
            //    return NotFound(Errors.FriendRequestNotFound);
            //}
            return Ok();
        }

    }
}
