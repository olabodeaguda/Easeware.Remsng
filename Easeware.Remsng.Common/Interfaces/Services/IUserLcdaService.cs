using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IUserLcdaService
    {
        Task<bool> Add(UserLcdaModel userLcdaModel);
        Task<UserLcdaModel> Get(UserLcdaModel userLcdaModel);
        Task<bool> Remove(UserLcdaModel userLcdaModel);
    }
}
