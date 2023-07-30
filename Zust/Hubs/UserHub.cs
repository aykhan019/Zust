using Microsoft.AspNetCore.SignalR;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendMessageToUser(Message message)
        {
            await Clients.Users(new string[] { message.ReceiverUserId }).SendAsync(SignalRConstants.ReceiveMessage, message);
        }
    }
}
