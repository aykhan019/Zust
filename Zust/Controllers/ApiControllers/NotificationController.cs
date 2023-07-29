using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.NotificationAPI)]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notificationService, IUserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }

        [HttpGet(Routes.GetNotificationsOfUser)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsOfUser(string userId)
        {
            try
            {
                var notifications = (await _notificationService.GetAllNotificationsOfUserAsync(userId)).ToList();
                
                notifications.ForEach(async notification =>
                {
                    notification.ToUser = await _userService.GetUserByIdAsync(notification.ToUserId);
                    notification.FromUser = await _userService.GetUserByIdAsync(notification.FromUserId);
                });

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.SetNotificationRead)]
        public async Task<ActionResult<IEnumerable<Notification>>> SetNotificationRead(string notificationId)
        {
            try
            {
                var notification = await _notificationService.GetNotificationByIdAsync(notificationId);

                await _notificationService.UpdateNotificationIsReadAsync(notificationId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Routes.GetUnreadNotificationCount)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUnseenNotificationCount(string userId)
        {
            try
            {
                var count = await _notificationService.GetUnreadNotificationCountAsync(userId);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
