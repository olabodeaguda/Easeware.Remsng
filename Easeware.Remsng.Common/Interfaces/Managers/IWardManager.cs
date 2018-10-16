using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IWardManager
    {
        Task<ResponseModel> AddAsync(WardModel wardModel);
        Task<ResponseModel> UpdateAsync(WardModel wardModel);
        Task<List<WardModel>> GetByLcdaAsync(long lcdaId);
        Task<PageModel> GetAsync(PageModel pageModel, long lcdaId);
        Task<WardModel> GetAsync(string wardName);
        Task<WardModel> GetByIdAsync(long wardId);
        Task<ResponseModel> UpdateStatusAsync(WardModel wardModel);
        Task<long> LastId();
    }
}
