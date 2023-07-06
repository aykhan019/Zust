namespace Zust.Web.Helpers.Constants
{
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
    }
}
