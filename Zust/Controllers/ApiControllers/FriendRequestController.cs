using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.UserHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.FriendRequest)]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

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

        [HttpGet(Routes.GetSentFriendRequests)]
        public async Task<IEnumerable<FriendRequest>> GetSentFriendRequests(string userId)
        {
            var friendRequests = await _friendRequestService.GetAllAsync(f => f.SenderId == userId);
            return friendRequests;
        }
    }
}
