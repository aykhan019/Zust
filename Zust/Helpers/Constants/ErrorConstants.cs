namespace Zust.Web.Helpers.Constants
{
    public class ErrorConstants
    {
        /// <summary>
        /// Represents the key for the username-related error.
        /// </summary>
        public const string UsernameError = "Username";

        /// <summary>
        /// Represents the error message for when a username already exists.
        /// </summary>
        public const string UsernameIsTakenError = "Username is already taken";


        public const string PasswordError = "Password";

        /// <summary>
        /// Represents the key for the login-related error.
        /// </summary>
        public const string LoginError = "Login";

        /// <summary>
        /// Represents the error message for an invalid login attempt.
        /// </summary>
        public const string InvalidLoginError = "Invalid Username or Password!";

        /// <summary>
        /// Represents the key for the role-related error.
        /// </summary>
        public const string RoleError = "Role";

        /// <summary>
        /// Represents the error message for when a role cannot be added.
        /// </summary>
        public const string CannotAddRoleError = "Role Cannot Be Added";


        public const string RegisterError = "An error occurred during the registration process";
    }
}
