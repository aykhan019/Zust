using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
using Zust.Web.Models;

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
        /// Gets the friend request service used by the controller.
        /// </summary>
        private readonly IFriendRequestService _friendRequestService;

        /// <summary>
        /// Gets the user service used by the controller.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Gets the friendship service used by the controller.
        /// </summary>
        private readonly IFriendshipService _friendshipService;

        /// <summary>
        /// Gets the notification service used by the controller.
        /// </summary>
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendRequestController"/> class with the specified services.
        /// </summary>
        /// <param name="friendRequestService">The friend request service to be used by the controller.</param>
        /// <param name="userService">The user service to be used by the controller.</param>
        /// <param name="friendshipService">The friendship service to be used by the controller.</param>
        /// <param name="notificationService">The notification service to be used by the controller.</param>
        public FriendRequestController(IFriendRequestService friendRequestService, IUserService userService, IFriendshipService friendshipService, INotificationService notificationService)
        {
            _friendRequestService = friendRequestService;

            _userService = userService;

            _friendshipService = friendshipService;

            _notificationService = notificationService;
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

                var exists = await _friendRequestService.CheckFriendRequestExistsAsync(currentUser.Id, receiverId, Status.Pending);

                if (exists)
                {
                    throw new Exception(Errors.FriendRequestAlreadySent);
                }

                var friendRequest = new FriendRequest()
                {
                    Id = Guid.NewGuid().ToString(),

                    SenderId = currentUser.Id,

                    ReceiverId = receiverId,

                    RequestDate = DateTime.Now,

                    Status = Status.Pending
                };

                await _friendRequestService.AddAsync(friendRequest);

                var notification = new Notification()
                {
                    Id = Guid.NewGuid().ToString(),

                    Date = DateTime.Now,

                    IsRead = false,

                    FromUserId = currentUser.Id,

                    ToUserId = receiverId,

                    Message = NotificationType.GetNewFriendRequestMessage(currentUser.UserName),
                };

                await _notificationService.AddAsync(notification);

                var sender = await _userService.GetUserByIdAsync(currentUser.Id);

                var reciever = await _userService.GetUserByIdAsync(receiverId);

                friendRequest.Sender = sender;

                friendRequest.Receiver = reciever;

                notification.FromUser = sender;

                notification.ToUser = reciever;

                var friendRequestNotificationVm = new FriendRequestNotificiationViewModel()
                {
                     Notification = notification,

                     FriendRequest = friendRequest
                };

                return Ok(friendRequestNotificationVm);
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

                if (friendRequests != null)
                {
                    return Ok(friendRequests);
                }
                else
                {
                    return BadRequest(Errors.AnErrorOccured);
                }
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

                var friendRequestsList = friendRequests.Where(fr => fr.Status == Status.Pending).ToList();

                foreach (var fr in friendRequestsList)
                {
                    fr.Sender = await _userService.GetUserByIdAsync(fr.SenderId);

                    fr.Receiver = await _userService.GetUserByIdAsync(fr.ReceiverId);
                }

                return Ok(friendRequestsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the count of pending received friend requests for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user whose friend requests are to be checked.</param>
        /// <returns>The count of pending received friend requests.</returns>
        [HttpGet(Routes.GetPendingReceivedFriendRequestsCount)]
        public async Task<ActionResult<int>> GetPendingReceivedFriendRequestsCount(string userId)
        {
            try
            {
                var friendRequests = await _friendRequestService.GetAllAsync(f => f.ReceiverId == userId);

                var result = friendRequests.Where(fr => fr.Status == Status.Pending);

                return Ok(result.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Accepts a friend request with the specified ID.
        /// </summary>
        /// <param name="requestId">The ID of the friend request to be accepted.</param>
        /// <returns>An IActionResult representing the status of the accept request operation.</returns>
        [HttpPost(Routes.AcceptRequest)]
        public async Task<IActionResult> AcceptRequest(string requestId)
        {
            try
            {
                var friendRequest = await _friendRequestService.GetAsync(fr => fr.Id == requestId);

                friendRequest.Receiver = await _userService.GetUserByIdAsync(friendRequest.ReceiverId);

                if (friendRequest != null)
                {
                    friendRequest.Status = Status.Accepted;

                    await _friendRequestService.UpdateAsync(friendRequest);

                    var friendShip = new Friendship()
                    {
                        FriendshipId = Guid.NewGuid().ToString(),

                        FriendId = friendRequest.ReceiverId,

                        UserId = friendRequest.SenderId
                    };

                    await _friendshipService.AddFriendship(friendShip);

                    var notification = new Notification()
                    {
                        Id = Guid.NewGuid().ToString(),

                        Date = DateTime.Now,

                        IsRead = false,

                        FromUserId = friendRequest.ReceiverId,

                        ToUserId = friendRequest.SenderId,

                        Message = NotificationType.GetFriendRequestAcceptedMessage(friendRequest.Receiver.UserName),
                    };

                    await _notificationService.AddAsync(notification);

                    notification.ToUser = await _userService.GetUserByIdAsync(notification.ToUserId);

                    notification.FromUser = await _userService.GetUserByIdAsync(notification.FromUserId);

                    return Ok(notification);
                }
                else
                {   
                    return NotFound(Errors.FriendRequestNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Declines a friend request with the specified ID.
        /// </summary>
        /// <param name="requestId">The ID of the friend request to be declined.</param>
        /// <returns>An IActionResult representing the status of the decline request operation.</returns>
        [HttpPost(Routes.DeclineRequest)]
        public async Task<IActionResult> DeclineRequest(string requestId)
        {
            try
            {
                var friendRequest = await _friendRequestService.GetAsync(fr => fr.Id == requestId);

                friendRequest.Receiver = await _userService.GetUserByIdAsync(friendRequest.ReceiverId);

                if (friendRequest != null)
                {
                    friendRequest.Status = Status.Declined;

                    await _friendRequestService.UpdateAsync(friendRequest);

                    var notification = new Notification()
                    {
                        Id = Guid.NewGuid().ToString(),

                        Date = DateTime.Now,

                        IsRead = false,

                        FromUserId = friendRequest.ReceiverId,

                        ToUserId = friendRequest.SenderId,

                        Message = NotificationType.GetFriendRequestDeclinedMessage(friendRequest.Receiver.UserName),
                    };

                    await _notificationService.AddAsync(notification);

                    notification.ToUser = await _userService.GetUserByIdAsync(notification.ToUserId);

                    notification.FromUser = await _userService.GetUserByIdAsync(notification.FromUserId);

                    return Ok(notification);
                }
                else
                {
                    return NotFound(Errors.FriendRequestNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
