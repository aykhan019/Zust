namespace Zust.Helpers
{
    public class Constants
    {
        public const string ConnectionStringName = "Default";
        public const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZustDb;Integrated Security=True;";
        public const string TokenSection = "AppSettings:Token";
        public const string MigrationsAssembly = "Zust.DataAccess";
        public const int TokenExpiry = 7; // In Days
    }
}
