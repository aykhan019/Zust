namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// Contains static methods to generate notification messages for various event types.
    /// </summary>
    public class NotificationType
    {
        /// <summary>
        /// Generates a notification message for a new friend request.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetNewFriendRequestMessage(string username)
        {
            return $"You have received a friend request from {username}!";
        }

        /// <summary>
        /// Generates a notification message for a friend request being accepted.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetFriendRequestAcceptedMessage(string username)
        {
            return $"{username} accepted your friend request!";
        }

        /// <summary>
        /// Generates a notification message for a friend request being declined.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetFriendRequestDeclinedMessage(string username)
        {
            return $"{username} declined your friend request!";
        }

        /// <summary>
        /// Generates a notification message for someone liking your post.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetLikedYourPostMessage(string username)
        {
            return $"{username} liked your post!";
        }

        /// <summary>
        /// Generates a notification message for someone commenting on your post.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetCommentedOnYourPostMessage(string username)
        {
            return $"{username} commented on your post!";
        }

        /// <summary>
        /// Generates a notification message for someone sending you a message.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetSentYouMessageMessage(string username)
        {
            return $"{username} sent you a message!";
        }

        /// <summary>
        /// Generates a notification message for someone sharing a post.
        /// </summary>
        /// <param name="username">The username of the sender.</param>
        /// <returns>The notification message.</returns>
        public static string GetSharedPostMessage(string username)
        {
            return $"{username} shared a post!";
        }
    }
}
