using Microsoft.AspNetCore.SignalR;
using Zust.Web.Helpers.ConstantHelpers;

namespace Zust.Web.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendMessageToUser(string userId, string text)
        {
            await Clients.Users(new string[] { userId }).SendAsync(SignalRConstants.ReceiveMessage, text);
        }
    }
}
