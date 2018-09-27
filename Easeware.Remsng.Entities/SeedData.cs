using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Entities
{
    public class SeedData
    {
        public static void Initialized(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<RemsDbContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
