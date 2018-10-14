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
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<EmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<JwtConfiguration>(Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>());
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<ILicenceService, LicenceService>();
            services.AddTransient<ILcdaService, LcdaService>();
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<ICodeGeneratorService, CodeGeneratorService>();
            services.AddTransient<IUserLcdaService, UserLcdaService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVerificationService, VerificationService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IWardService, WardService>();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "MemoryCache";
            });
        }
    }
}
