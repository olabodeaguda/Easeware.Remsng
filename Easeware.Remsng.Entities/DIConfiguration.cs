using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Easeware.Remsng.Entities
{
    public static class DIConfiguration
    {
        public static void InitializeEntities(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddDbContextPool<RemsDbContext>(options => options
                  .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            try
            {
                SeedData.Initialized(services.BuildServiceProvider());
            }
            catch (Exception x)
            {
                Log.Error(x, "Error while seeding");
            }
        }
    }
}
