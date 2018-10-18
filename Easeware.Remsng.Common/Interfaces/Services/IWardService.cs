using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IWardService
    {
        Task<ResponseModel> AddAsync(WardModel wardModel);
        Task<ResponseModel> UpdateAsync(WardModel wardModel);
        Task<List<WardModel>> GetByLcdaAsync(string lcdaCode);
        Task<PageModel> GetAsync(PageModel pageModel, string lcdaCode);
        Task<WardModel> GetByIdAsync(long wardId);
        Task<ResponseModel> UpdateStatusAsync(WardModel wardModel);
        Task<WardModel> GetAsync(string wardName);
        Task<long> LastId();
    }
}
