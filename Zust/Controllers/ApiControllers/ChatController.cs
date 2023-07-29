using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
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

        public ChatController(IChatService chatService, IMessageService messageService, IUserService userService)
        {
            _chatService = chatService;

            _messageService = messageService;

            _userService = userService;
        }

        [HttpPost(Routes.AddMessage)]
        public async Task<ActionResult<Message>> AddMessage([FromBody] MessageViewModel model)
        {
            try
            {
                // For Current User
                var message = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = model.Text,
                    ReceiverUserId = model.ReceiverUserId,
                    SenderUserId = model.SenderUserId,
                    ChatId = model.ChatId,
                    DateSent = model.DateSent
                };

                await _messageService.AddMessageAsync(message);

                message.ReceiverUser = await _userService.GetUserByIdAsync(model.ReceiverUserId);
                message.SenderUser = await _userService.GetUserByIdAsync(model.SenderUserId);
                message.Chat = await _chatService.GetChatByIdAsync(model.ChatId);

                // For User To Send Message
                var otherUserChat = await _chatService.GetChatAsync(model.ReceiverUserId, model.SenderUserId);

                var message2 = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = model.Text,
                    ReceiverUserId = model.ReceiverUserId,
                    SenderUserId = model.SenderUserId,
                    ChatId = otherUserChat.Id,
                    DateSent = model.DateSent
                };

                await _messageService.AddMessageAsync(message2);

                // Success
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
