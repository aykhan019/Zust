using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zust.DataAccess.Helpers;
using Zust.Entities.Models;

namespace Zust.Core.Concrete.EntityFramework
{
    /// <summary>
    /// Represents the DbContext for the Zust application, derived from IdentityDbContext with custom User, Role, and primary key type.
    /// </summary>
    public partial class ZustDbContext : IdentityDbContext<User, Role, string>
    {
        /// <summary>
        /// Initializes a new instance of the ZustDbContext class with the specified DbContextOptions.
        /// </summary>
        /// <param name="contextOptions">The options for configuring the DbContext.</param>
        public ZustDbContext(DbContextOptions<ZustDbContext> contextOptions) : base(contextOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ZustDbContext class.
        /// </summary>
        public ZustDbContext() 
        {
        }

        /// <summary>
        /// Overrides the default configuration of the DbContext options.
        /// Sets the database connection based on the appsettings.json file.
        /// </summary>
        /// <param name="optionsBuilder">The DbContext options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configBuilder = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(Constants.AppSettingsFile, optional: true, reloadOnChange: true);

                IConfiguration configuration = configBuilder.Build();

                // Read the connection string
                string connectionString = configuration.GetConnectionString(Constants.Default);

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Represents a DbSet for managing Friendships in the database.
        /// </summary>
        public DbSet<Friendship>? Friendships { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Posts in the database.
        /// </summary>
        public DbSet<Post>? Posts { get; set; }

        /// <summary>
        /// Represents a DbSet for managing FriendRequests in the database.
        /// </summary>
        public DbSet<FriendRequest>? FriendRequest { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Notifications in the database.
        /// </summary>
        public DbSet<Notification>? Notifications { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Likes in the database.
        /// </summary>
        public DbSet<Like>? Likes { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Comments in the database.
        /// </summary>
        public DbSet<Comment>? Comments { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Chats in the database.
        /// </summary>
        public DbSet<Chat>? Chat { get; set; }

        /// <summary>
        /// Represents a DbSet for managing Messages in the database.
        /// </summary>
        public DbSet<Message>? Messages { get; set; }
    }
}
