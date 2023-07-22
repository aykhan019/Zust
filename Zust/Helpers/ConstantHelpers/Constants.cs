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
        /// Represents the file name for covers.
        /// </summary>

        public const string CloudinarySettings = "CloudinarySettings";

        public const string ImageFileType = "image/";

        public const string VideoFileType = "video/";

        public const string NoContentImageUrl = "https://res.cloudinary.com/dax9yhk8g/image/upload/v1689665492/noContentFound_m74is3.png";

        public const int StatusCountInNewsFeed = 6;

        public const int VideoCountInNewsFeed = 6;

        public const int AdvertisementCountInNewsFeed = 2;

        public const int BirthdayRange = 7;

    }
}       