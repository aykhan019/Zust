using Microsoft.AspNetCore.SignalR;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Hubs
{
    /// <summary>
    /// SignalR hub for handling real-time communication with connected users.
    /// </summary>
    public class UserHub : Hub
    {
        // There are repetitions . . .

        /// <summary>
        /// Sends a message to a specific user.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public async Task SendMessageToUser(Message message)
        {
            await Clients.Users(new string[] { message.ReceiverUserId }).SendAsync(SignalRConstants.ReceiveMessage, message);
        }

        /// <summary>
        /// Sends a notification and friend request response to a specific user.
        /// </summary>
        /// <param name="notification">The notification to be sent.</param>
        /// <param name="friendRequestViewModel">The view model containing friend request data.</param>
        public async Task SendNotification(Notification notification, FriendRequestViewModel friendRequestViewModel)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);

            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveFriendRequestResponse, friendRequestViewModel);
        }

        /// <summary>
        /// Sends a comment notification to a specific user.
        /// </summary>
        /// <param name="notification">The notification to be sent.</param>
        public async Task SendCommentNotification(Notification notification)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);
        }

        /// <summary>
        /// Sends a friend request to a specific user.
        /// </summary>
        /// <param name="friendRequest">The friend request to be sent.</param>
        public async Task SendFriendRequest(FriendRequest friendRequest)
        {
            await Clients.Users(new string[] { friendRequest.ReceiverId }).SendAsync(SignalRConstants.ReceiveFriendRequest, friendRequest);
        }

        /// <summary>
        /// Sends a friend request notification to a specific user.
        /// </summary>
        /// <param name="notification">The notification to be sent.</param>
        public async Task SendFriendRequestNotification(Notification notification)
        {
            await Clients.Users(new string[] { notification.ToUserId }).SendAsync(SignalRConstants.ReceiveNotification, notification);
        }
    }
}
