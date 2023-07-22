namespace Zust.Web.Helpers.ConstantHelpers
{
    public class NotificationType
    {
        public const string NewFriendRequest = "New Friend Request";
        
        public static string GetNewFriendRequestMessage(string username)
        {
            return $"You have received a friend request from {username}!";
        }

        public const string FriendRequestAccepted = "Friend Request Accepted";

        public static string GetFriendRequestAcceptedMessage(string username)
        {
            return $"{username} accepted your friend request!";
        }
    }
}
