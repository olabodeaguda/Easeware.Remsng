using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Data.Implementations;
using Easeware.Remsng.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Data
{
    public static class DIConfiguration
    {
        public static void InitializeData(this IServiceCollection services,
          IConfiguration Configuration)
        {
            services.InitializeEntities(Configuration);
            services.AddTransient<ILcdaManager, LcdaManager>();
            services.AddTransient<IUserLcdaManager, UserLcdaManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IVerificationManager, VerificationManager>();
        }
    }
}
