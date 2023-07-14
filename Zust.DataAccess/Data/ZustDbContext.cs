using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<FriendRequest>? FriendRequest { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
    }
}
