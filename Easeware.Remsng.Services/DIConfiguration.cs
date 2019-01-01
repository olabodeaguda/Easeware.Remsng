using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Data;
using Easeware.Remsng.Infrastructure.Managers;
using Easeware.Remsng.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Easeware.Remsng.Infrastructure
{
    public static class DIConfiguration
    {
        public static void InitializeServices(this IServiceCollection services,
         IConfiguration Configuration)
        {
            services.InitializeData(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<EmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<JwtConfiguration>(Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>());
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ILicenceService, LicenceService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ISectorService, SectorService>();

            // Managers
            services.AddScoped<ILcdaManager, LcdaManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IWardManager, WardManager>();
            services.AddScoped<IStreetManager, StreetManager>();
            services.AddScoped<ICompanyManager, CompanyManager>();
            services.AddScoped<ITaxpayerManager, TaxpayerManager>();
            services.AddScoped<IAddressManager, AddressManager>();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "MemoryCache";
            });
        }
    }
}
