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

        public static string GetFriendRequestDeclinedMessage(string username)
        {
            return $"{username} declined your friend request!";
        }

        public static string GetLikedYourPostMessage(string username)
        {
            return $"{username} liked your post!";
        }

        public static string GetCommentedOnYourPostMessage(string username)
        {
            return $"{username} commented on your post!";
        }

        public static string GetSentYouMessageMessage(string username)
        {
            return $"{username} sent you a message!";
        }

        public static string GetSharedPostMessage(string username)
        {
            return $"{username} shared a post!";
        }
    }
}
