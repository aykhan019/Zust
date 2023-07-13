namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// A collection of constants in the application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents the name of the connection string in the configuration.
        /// </summary>
        public const string ConnectionStringName = "Default";

        /// <summary>
        /// Represents the connection string used to connect to the database.
        /// </summary>
        public const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZustDb;Integrated Security=True;";

        /// <summary>
        /// Represents the name of the assembly containing database migrations.
        /// </summary>
        public const string MigrationsAssembly = "Zust.Web";

        /// <summary>
        /// Represents the expiration time span for a cookie in days.
        /// </summary>
        public const int CookieExpireTimeSpan = 30;

        /// <summary>
        /// Represents the default file path for user profile images.
        /// </summary>
        public const string DefaultProfileImagePath = "/assets/images/user/defaultUserImage.png";

        /// <summary>
        /// Represents the "No Data" string.
        /// </summary>
        public const string NoData = "No Data";

        /// <summary>
        /// Represents the number of users to take in user-related operations.
        /// </summary>
        public const int TakeUserCount = 8;

        /// <summary>
        /// Represents an empty string.
        /// </summary>
        public const string StringEmpty = "";

        /// <summary>
        /// Represents the folder path for files.
        /// </summary>
        public const string FilesFolderPath = "~/../Files";

        /// <summary>
        /// Represents the file name for covers.
        /// </summary>
        public const string CoversFile = "covers.txt";
    }
}