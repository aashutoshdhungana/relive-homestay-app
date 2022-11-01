using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relive.Server.Infrastructure.Data;

namespace Relive.Server.Infrastructure
{
    public static class ConfigureDependencies
    {
        // Use this to configure your app with the minimum dependency
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("SqlServerConnection");
                options.UseSqlServer(connectionString);
            });
        }
    }
}
