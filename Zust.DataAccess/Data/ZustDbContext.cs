using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Core.Concrete.EntityFramework
{
    public partial class ZustDbContext : IdentityDbContext<User, Role, string>
    {
        public ZustDbContext(DbContextOptions<ZustDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        public ZustDbContext()
        {

        }

        public DbSet<User>? Users { get; set; }
    }
}
