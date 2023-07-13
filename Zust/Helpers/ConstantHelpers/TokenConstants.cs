namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// Contains constant values related to tokens used in the application.
    /// </summary>
    public static class TokenConstants
    {
        /// <summary>
        /// Represents the configuration section key for the token in the app settings.
        /// </summary>
        public const string TokenSection = "AppSettings:Token";

        /// <summary>
        /// Represents the token expiry duration in days.
        /// </summary>
        public const int TokenExpiry = 7;

        /// <summary>
        /// Represents the key used to store the token in the session.
        /// </summary>
        public const string MyToken = "MyToken";
    }
}
