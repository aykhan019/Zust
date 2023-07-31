using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
using Zust.Web.Migrations;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.ChatAPI)]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly INotificationService _notificationService;

        public ChatController(IChatService chatService, IMessageService messageService, IUserService userService, INotificationService notificationService)
        {
            _chatService = chatService;

            _messageService = messageService;

            _userService = userService;

            _notificationService = notificationService;
        }

        [HttpPost(Routes.AddMessage)]
        public async Task<ActionResult<Message>> AddMessage([FromBody] SendMessageViewModel model)
        {
            try
            {
                // For Current User
                var message = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = model.Message.Text,
                    ReceiverUserId = model.Message.ReceiverUserId,
                    SenderUserId = model.Message.SenderUserId,
                    ChatId = model.Message.ChatId,
                    DateSent = DateTime.Now
                };

                await _messageService.AddMessageAsync(message);

                message.ReceiverUser = await _userService.GetUserByIdAsync(model.Message.ReceiverUserId);
                message.SenderUser = await _userService.GetUserByIdAsync(model.Message.SenderUserId);
                message.Chat = await _chatService.GetChatByIdAsync(model.Message.ChatId);

                // For User To Send Message
                var otherUserChat = await _chatService.GetChatAsync(model.Message.ReceiverUserId, model.Message.SenderUserId);

                var message2 = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = model.Message.Text,
                    ReceiverUserId = model.Message.ReceiverUserId,
                    SenderUserId = model.Message.SenderUserId,
                    ChatId = otherUserChat.Id,
                    DateSent = DateTime.Now
                };

                await _messageService.AddMessageAsync(message2);

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var messageNotificationVM = new MessageNotificationViewModel()
                {
                     Message = message,
                      Notification = null
                };

                if (!model.FirstMessageSent)
                {

                    var notification = new Notification()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Date = DateTime.Now,
                        IsRead = false,
                        FromUserId = currentUser.Id,
                        FromUser = currentUser,
                        ToUserId = model.Message.ReceiverUserId,
                        ToUser = await _userService.GetUserByIdAsync(model.Message.ReceiverUserId),
                        Message = NotificationType.GetSentYouMessageMessage(currentUser.UserName),
                    };

                    messageNotificationVM.Notification = notification;

                    await _notificationService.AddAsync(notification);
                }

                // Success
                return Ok(messageNotificationVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetChats)]
        public async Task<ActionResult<IEnumerable<User>>> GetChats(string userId)
        {
            try
            {
                var chats = await _chatService.GetAllUserChats(userId);
                var list = chats.ToList();
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var usersTasks = list.Select(async c =>
                {
                    if (c.SenderUserId != currentUser.Id)
                    {
                        return await _userService.GetUserByIdAsync(c.SenderUserId);
                    }
                    else if (c.ReceiverUserId != currentUser.Id)
                    {
                        return await _userService.GetUserByIdAsync(c.ReceiverUserId);
                    }

                    return null; // Return null for cases when neither SenderUserId nor ReceiverUserId matches the currentUser's Id
                });

                var users = await Task.WhenAll(usersTasks);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
