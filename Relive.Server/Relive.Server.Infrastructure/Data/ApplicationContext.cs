using Microsoft.EntityFrameworkCore;
using Relive.Server.Core.UserAggregate;

namespace Relive.Server.Infrastructure.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
