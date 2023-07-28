namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// Contains constant values for various URLs used in the application.
    /// </summary>
    public static class Routes
    {
        public const string Home = "home";

        public const string Account = "account";

        public const string ForgotPassword = "forgot-password";

        public const string HelpAndSupport = "help-and-support";

        public const string LiveChat = "live-chat";

        public const string MyProfile = "my-profile";

        public const string UserProfile = "user-profile";

        public const string FriendRequests = "friend-requests";

        public const string Register = "register";

        public const string Users = "users";

        public const string Login = "login";

        public const string Index = "index";

        public const string Error = "Error";
            
        public const string UserExistsRoute = "userexists/{username}";

        public const string Authentication = "Authentication";

        public const string ProfileController = "api/profile";

        public const string LikeController = "api/like";

        public const string UpdateProfile = "UpdateProfile";

        public const string UserController = "api/User";

        public const string GetUsers = "GetUsers";

        public const string GetUsersByText = "GetUsersByText/{text}";

        public const string GetUser = "GetUser/{id}";

        public const string GetAllUsersCount = "GetAllUsersCount";

        public const string FriendRequest = "api/FriendRequest";

        public const string AddFriendRequest = "AddFriendRequest";

        public const string CancelFriendRequest = "CancelFriendRequest";

        public const string GetSentFriendRequests = "GetSentFriendRequests";

        public const string GetReceivedFriendRequests = "GetReceivedFriendRequests";

        public const string AcceptRequest = "AcceptRequest";

        public const string DeclineRequest = "DeclineRequest";

        public const string GetFollowers = "GetFollowers";

        public const string GetFollowersCount = "GetFollowersCount";

        public const string GetFollowings = "GetFollowings";

        public const string GetFollowingsCount = "GetFollowingsCount";

        public const string RemoveFriend = "RemoveFriend";

        public const string RemoveFollower = "RemoveFollower";

        public const string GetCurrentUser = "GetCurrentUser";

        public const string PostAPI = "api/Post";

        public const string CreatePost = "CreatePost";

        public const string GetAllPosts = "GetAllPosts";

        public const string GetAllPostsOfUser = "GetAllPostsOfUser";

        public const string Static = "api/Static";

        public const string GetRandomStatusImagePaths = "GetRandomStatusImagePaths";

        public const string GetWatchVideos = "GetWatchVideos";

        public const string UpdateProfileImage = "UpdateProfileImage";

        public const string GetSpecialUsers = "GetSpecialUsers";

        public const string GetUsersWithTodayBirthday = "GetUsersWithTodayBirthday";

        public const string GetUsersWithRecentBirthday = "GetUsersWithRecentBirthday";

        public const string GetUsersWithComingBirthday = "GetUsersWithComingBirthday";

        public const string Post = "post";

        public const string GetPostLikeCount = "GetPostLikeCount";

        public const string LikePost = "LikePost";

        public const string UnlikePost = "UnlikePost";

        public const string GetPostIdsLiked = "GetPostIdsLiked";

        public const string GetRandomFollowers = "GetRandomFollowers";

        public const string GetAllPostsLikeCount = "GetAllPostsLikeCount";

        public const string GetFollowersInRange = "GetFollowersInRange";

        public const string GetFollowingsInRange = "GetFollowingsInRange";

        public const string NotificationAPI = "api/Notification";

        public const string GetNotificationsOfUser = "GetNotificationsOfUser";

        public const string UserLikedPost = "UserLikedPost";

        public const string GetCommentsOfPost = "GetCommentsOfPost";

        public const string GetCountOfCommentsOfPost = "GetCountOfCommentsOfPost/{postId}";

        public const string AddComment = "AddComment";

        public const string GetPendingReceivedFriendRequestsCount = "GetPendingReceivedFriendRequestsCount";

        public const string SetNotificationSeemed = "SetNotificationSeemed";

        public const string GetUnseenNotificationCount = "GetUnseenNotificationCount";
    }
}
