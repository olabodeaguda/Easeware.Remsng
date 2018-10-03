using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<bool> Add(UserModel userModel);
        Task<UserModel> Get(string email);
        Task<UserModel> Get(long id);
        Task<PageModel> Get(PageModel pageModel, long Lcdaid);
        Task<PageModel> Get(PageModel pageModel, string lcdaCode);
    }
}
