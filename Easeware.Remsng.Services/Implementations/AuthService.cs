using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class AuthService : IAuthService
    {
        IDistributedCache _distributedCache;
        private JwtConfiguration _jwtConfiguration;
        public AuthService(IDistributedCache distributedCache, JwtConfiguration jwtConfiguration)
        {
            _distributedCache = distributedCache;
            _jwtConfiguration = jwtConfiguration;
        }

        public async Task LogAccess(LoginResponseModel loginResponseModel)
        {
            await _distributedCache.SetStringAsync(loginResponseModel.accessToken,
                JsonConvert.SerializeObject(loginResponseModel), new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_jwtConfiguration.TokenLifespan)
                });
            await _distributedCache.SetStringAsync(loginResponseModel.refreshToken,
               JsonConvert.SerializeObject(loginResponseModel), new DistributedCacheEntryOptions()
               {
                   AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_jwtConfiguration.SessionLifespan)
               });
        }

        public async Task RefreshSession(LoginResponseModel loginResponseModel)
        {
            await _distributedCache.SetStringAsync(loginResponseModel.refreshToken,
              JsonConvert.SerializeObject(loginResponseModel), new DistributedCacheEntryOptions()
              {
                  AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_jwtConfiguration.SessionLifespan)
              });
        }

        public async Task Remove(LoginResponseModel loginResponseModel)
        {
            await _distributedCache.RefreshAsync(loginResponseModel.accessToken);
            await _distributedCache.RefreshAsync(loginResponseModel.refreshToken);
        }

        public async Task<LoginResponseModel> SessionLog(string refreshToken)
        {
            string result = await _distributedCache.GetStringAsync(refreshToken);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<LoginResponseModel>(result);
        }


    }
}
