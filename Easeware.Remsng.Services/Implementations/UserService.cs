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
        public UserService()
        {

        }
        public Task<bool> Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> Get(PageModel pageModel, long Lcdaid)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> Get(PageModel pageModel, string lcdaCode)
        {
            throw new NotImplementedException();
        }
    }
}
