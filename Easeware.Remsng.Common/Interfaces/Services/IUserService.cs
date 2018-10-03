using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> Add(UserModel userModel);
        Task<UserModel> Get(string email);
        Task<UserModel> Get(long id);
        Task<List<UserModel>> Get(PageModel pageModel, long Lcdaid);
        Task<List<UserModel>> Get(PageModel pageModel, string lcdaCode);

    }
}
