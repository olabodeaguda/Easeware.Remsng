using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class UserLcdaService : IUserLcdaService
    {
        private IUserLcdaManager _userlcdaManager;
        public UserLcdaService(IUserLcdaManager userLcdaManager)
        {
            _userlcdaManager = userLcdaManager;
        }
        public async Task<bool> Add(UserLcdaModel userLcdaModel)
        {
            UserLcdaModel uLModel = await _userlcdaManager.Get(userLcdaModel);
            if (uLModel != null)
            {
                throw new BadRequestException("User already exist in selected lcda");
            }
            return await _userlcdaManager.Add(userLcdaModel);
        }
        public async Task<UserLcdaModel> Get(UserLcdaModel userLcdaModel)
        {
            return await _userlcdaManager.Get(userLcdaModel);
        }

        public async Task<bool> Remove(UserLcdaModel userLcdaModel)
        {
            UserLcdaModel uLModel = await _userlcdaManager.Get(userLcdaModel);
            if (uLModel == null)
            {
                throw new BadRequestException("User does not exist");
            }

            return await _userlcdaManager.Remove(uLModel);
        }
    }
}
