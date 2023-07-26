namespace Zust.Web.Helpers.ConstantHelpers
{
    public class NotificationType
    {
        public static string GetNewFriendRequestMessage(string username)
        {
            return $"You have received a friend request from {username}!";
        }

        public static string GetFriendRequestAcceptedMessage(string username)
        {
            return $"{username} accepted your friend request!";
        }
    }
}
