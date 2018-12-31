using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Repositories;
using Easeware.Remsng.Data.Repositories;
using Easeware.Remsng.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Easeware.Remsng.Data
{
    public static class DIConfiguration
    {
        public static void InitializeData(this IServiceCollection services,
          IConfiguration Configuration)
        {
            services.InitializeEntities(Configuration);
            services.AddTransient<ILcdaRepository, LcdaRepository>();
            services.AddTransient<IUserLcdaRepository, UserLcdaRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVerificationRepository, VerificationRepository>();
            services.AddTransient<IWardRepository, WardRepository>();
            services.AddTransient<ISectorRepository, SectorRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IStreetRepository, StreetRepository>();
        }
    }
}
