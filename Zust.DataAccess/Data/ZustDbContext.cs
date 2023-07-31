using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zust.Entities.Models;

namespace Zust.Core.Concrete.EntityFramework
{
    public partial class ZustDbContext : IdentityDbContext<User, Role, string>
    {
        public ZustDbContext(DbContextOptions<ZustDbContext> contextOptions) : base(contextOptions) {
        }

        public ZustDbContext() {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZustDb;Integrated Security=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Friendship>? Friendships { get; set; }
        public DbSet<Post>? Posts{ get; set; }
        public DbSet<FriendRequest>? FriendRequest { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Like>? Likes { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Chat>? Chat { get; set; }
        public DbSet<Message>? Messages { get; set; }
    }
}
