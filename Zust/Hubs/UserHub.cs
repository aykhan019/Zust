using Microsoft.AspNetCore.SignalR;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Hubs
{
    public class UserHub : Hub
    {
        // There are repetitions . . .

        private readonly IUserService _userService;

        public UserHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SendMessageToUser(Message message)
        {
            await Clients.Users(new string[] { message.ReceiverUserId }).SendAsync(SignalRConstants.ReceiveMessage, message);
        }

        public async Task SendNotification(Notification notification, FriendRequestViewModel friendRequestViewModel)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);

            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveFriendRequestResponse, friendRequestViewModel);
        }

        public async Task SendCommentNotification(Notification notification)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);
        }

        public async Task SendFriendRequest(FriendRequest friendRequest)
        {
            await Clients.Users(new string[] { friendRequest.ReceiverId }).SendAsync(SignalRConstants.ReceiveFriendRequest, friendRequest);
        }

        public async Task SendFriendRequestNotification(Notification notification)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);
        }
    }
}
