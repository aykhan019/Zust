using Microsoft.AspNetCore.SignalR;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendMessageToUser(MessageViewModel model)
        {
            await Clients.Users(new string[] { model.ReceiverUserId }).SendAsync(SignalRConstants.ReceiveMessage, model);
        }
    }
}
