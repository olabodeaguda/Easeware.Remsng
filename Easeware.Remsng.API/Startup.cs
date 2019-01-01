using AutoMapper;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Easeware.Remsng.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.InitializeServices(Configuration);
            services.InitializeSecurity(services.BuildServiceProvider());

            services.AddApiVersioning(v => v.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ModelValidationAttribute));
                opt.Filters.Add(new GlobalExceptionFilter());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware(typeof(AccessChallengeMiddleware));
            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context.Response.ContentType = "application/json";
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        var result = context.Get(error.Error);
                        var res = JsonConvert.SerializeObject(result,
                                  new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        await context.Response.WriteAsync(res);
                    });
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
