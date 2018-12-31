using Easeware.Remsng.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Utilities
{
    public class AccessChallengeMiddleware
    {
        private readonly RequestDelegate next;
        public AccessChallengeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            IJwtService jwtService = (IJwtService)serviceProvider.GetService(typeof(IJwtService));
            try
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    string token = context.Request.Headers["Authorization"];
                    if (token != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        token = token.Replace("Bearer ", "");
                        token = token.Replace("bearer ", "");

                        JwtSecurityToken jst = handler.ReadJwtToken(token);

                        context.User = handler.ValidateToken(token,
                            (TokenValidationParameters)jwtService.ValidatorParameters(),
                            out SecurityToken validatedToken);
                        await next(context);
                    }
                    else
                    {
                        await next(context);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var val = context.Get(ex);
                var res = JsonConvert.SerializeObject(val,
                                  new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                await context.Response.WriteAsync(res);
            }
        }
    }
}
