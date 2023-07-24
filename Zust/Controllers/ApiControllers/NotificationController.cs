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
                var notifications = await _notificationService.GetAllNotificationsOfUserAsync(userId);

                notifications.ToList().ForEach(async notification =>
                {
                    notification.User = await _userService.GetUserByIdAsync(notification.UserId);
                });

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
