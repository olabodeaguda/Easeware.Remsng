using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// log access to distributed cache
        /// </summary>
        /// <param name="loginResponseModel"></param>
        /// <returns></returns>
        Task LogAccess(LoginResponseModel loginResponseModel);

        Task<LoginResponseModel> GetSession(string refreshToken);
        Task Remove(LoginResponseModel loginResponseModel);
        Task RefreshSession(LoginResponseModel loginResponseModel);
    }
}
