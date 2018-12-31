using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IWardRepository
    {
        Task<WardModel> AddAsync(WardModel wardModel);
        Task<WardModel> UpdateAsync(WardModel wardModel);
        Task<List<WardModel>> GetByLcdaAsync(string lcdaCode);
        Task<PageModel> GetAsync(PageModel pageModel, string lcdaCode);
        Task<WardModel> GetAsync(string wardName);
        Task<WardModel> GetAsync(string wardName, long lcdaId);
        Task<WardModel> GetByIdAsync(long wardId);
        Task<WardModel> UpdateStatusAsync(WardModel wardModel);
        Task<long> LastId();
    }
}
