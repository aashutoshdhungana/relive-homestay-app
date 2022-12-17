using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relive.Server.Infrastructure.Data;
using System;

namespace Relive.Server.Infrastructure
{
    public static class ConfigureDependencies
    {
        // Use this to configure your app with the minimum dependency
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                //string connectionString = configuration.GetConnectionString("SqlServerConnection");
                //options.UseSqlServer(connectionString);
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var dbPath = System.IO.Path.Join(path, "relive.db");
                options.UseSqlite($"Data Source={dbPath}");
            });
        }
    }
}
