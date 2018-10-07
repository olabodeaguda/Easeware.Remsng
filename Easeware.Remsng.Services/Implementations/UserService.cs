using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class UserService : IUserService
    {
        private IEncryptionService _encryptionService;
        private IUserManager _userManager;
        public UserService(IUserManager userManager, IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _encryptionService = encryptionService;
        }
        public Task<bool> Add(UserModel userModel)
        {
            userModel.passwordHash = _encryptionService.Encrypt(userModel.Password);
            return _userManager.Add(userModel);
        }

        public Task<UserModel> Get(string email)
        {
            return _userManager.Get(email);
        }

        public Task<UserModel> Get(long id)
        {
            return _userManager.Get(id);
        }

        public Task<PageModel> Get(PageModel pageModel, long Lcdaid)
        {
            return _userManager.Get(pageModel, Lcdaid);
        }

        public Task<PageModel> Get(PageModel pageModel, string lcdaCode)
        {
            return _userManager.Get(pageModel, lcdaCode);
        }

        public Task<bool> UpdateStatus(UserModel userModel)
        {
            return _userManager.UpdateStatus(userModel);
        }
    }
}
