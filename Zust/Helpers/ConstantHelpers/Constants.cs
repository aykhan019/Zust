namespace Zust.Web.Helpers.ConstantHelpers
{
    /// <summary>
    /// A collection constants in the application.
    /// </summary>
    public class Constants
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

        public const int CookieExpireTimeSpan = 30;

        public const string DefaultProfileImagePath = "/assets/images/user/defaultUserImage.png";

        public const string NoData = "No Data";

        public const int TakeUserCount = 8;

        public const string StringEmpty = "";

        public const string FilesFolderPath = "~/../Files";

        public const string CoversFile = "covers.txt";
    }
}
