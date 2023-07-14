namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// A collection of error constants for common error scenarios in the application.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Represents the key for the username-related error.
        /// </summary>
        public const string UsernameError = "Username";

        /// <summary>
        /// Represents the error message for when a username already exists.
        /// </summary>
        public const string UsernameIsTakenError = "Username is already taken!";

        /// <summary>
        /// Represents the key for the password-related error.
        /// </summary>
        public const string PasswordError = "Password";

        /// <summary>
        /// Represents the key for the login-related error.
        /// </summary>
        public const string LoginError = "Login";

        /// <summary>
        /// Represents the error message for an invalid login attempt.
        /// </summary>
        public const string InvalidLoginError = "Invalid username or password!";

        /// <summary>
        /// Represents the key for the role-related error.
        /// </summary>
        public const string RoleError = "Role";

        /// <summary>
        /// Represents the error message for when a role cannot be added.
        /// </summary>
        public const string CannotAddRoleError = "Role cannot be added!";

        /// <summary>
        /// Represents the error message for general registration error.
        /// </summary>
        public const string RegisterError = "An error occurred during the registration process.";

        /// <summary>
        /// Represents the error message for when a friend request is not found.
        /// </summary>
        public const string FriendRequestNotFound = "Friend request was not found";


        public const string AnErrorOccured = "An Error Occured";
    }
}
