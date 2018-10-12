using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IWardManager
    {
        Task<int> AddAsync(WardModel wardModel);
        Task<ResponseModel> UpdateAsync(WardModel wardModel);
        Task<List<WardModel>> GetByLcdaAsync(long lcdaId);
        Task<PageModel> GetAsync(PageModel pageModel, long lcdaId);
        Task<WardModel> GetByIdAsync(long wardId);
        Task<ResponseModel> UpdateStatusAsync(WardModel wardModel);

    }
}
