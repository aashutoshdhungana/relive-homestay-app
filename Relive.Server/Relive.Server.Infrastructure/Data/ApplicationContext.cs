using Microsoft.EntityFrameworkCore;
using Relive.Server.Core.Entities.UserEntities;

namespace Relive.Server.Infrastructure.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<HostUser> HostUsers { get; set; }
        public DbSet<TravelerUser> TravelerUsers { get; set; }
    }
}
