namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// Contains constant values for SignalR event names used in the application.
    /// </summary>
    public class SignalRConstants
    {
        /// <summary>
        /// Represents the SignalR event name for receiving a message.
        /// </summary>
        public const string ReceiveMessage = "ReceiveMessage";

        /// <summary>
        /// Represents the SignalR event name for receiving a notification.
        /// </summary>
        public const string ReceiveNotification = "ReceiveNotification";

        /// <summary>
        /// Represents the SignalR event name for receiving a friend request.
        /// </summary>
        public const string ReceiveFriendRequest = "ReceiveFriendRequest";

        /// <summary>
        /// Represents the SignalR event name for receiving a friend request response.
        /// </summary>
        public const string ReceiveFriendRequestResponse = "ReceiveFriendRequestResponse";
    }
}