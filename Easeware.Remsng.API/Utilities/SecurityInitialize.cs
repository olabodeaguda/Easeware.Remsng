using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Utilities
{
    public static class SecurityInitialize
    {
        public static void InitializeSecurity(this IServiceCollection services, IServiceProvider serviceProvider)
        {
            IJwtService jwtService = (IJwtService)serviceProvider.GetService(typeof(IJwtService));
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
            options =>
            {
                //options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.TokenValidationParameters = (TokenValidationParameters)jwtService.ValidatorParameters();
                options.IncludeErrorDetails = true;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        ResponseModel response = new ResponseModel();
                        Exception except = context.Exception;

                        var error = context.HttpContext.Features.Get<IExceptionHandlerFeature>();
                        var result = context.HttpContext.Get(error.Error);
                        var res = JsonConvert.SerializeObject(result,
                                  new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                        context.Fail(res);

                        return Task.FromException(except);// CompletedTask;
                    }
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());
            });
        }
    }
}
