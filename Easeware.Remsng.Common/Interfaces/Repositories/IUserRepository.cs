using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IUserRepository
    {
        Task<UserModel> Add(UserModel userModel);
        Task<UserModel> Get(string email);
        Task<UserModel> Get(long id);
        //Task<PageModel> Get(PageModel pageModel, long Lcdaid);
        Task<PageModel> Get(PageModel pageModel, string lcdaCode);
        Task<bool> UpdateStatus(UserModel userModel);
        Task<bool> ChangePassword(UserModel uModel);
    }
}
