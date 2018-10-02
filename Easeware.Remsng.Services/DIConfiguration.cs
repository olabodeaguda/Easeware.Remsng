using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Data;
using Easeware.Remsng.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Services
{
    public static class DIConfiguration
    {
        public static void InitializeServices(this IServiceCollection services,
         IConfiguration Configuration)
        {
            services.InitializeData(Configuration);
            services.AddSingleton<EmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<ILicenceService, LicenceService>();
            services.AddTransient<ILcdaService, LcdaService>();

        }
    }
}
