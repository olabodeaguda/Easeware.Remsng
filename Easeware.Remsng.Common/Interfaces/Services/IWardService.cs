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
        Task<List<WardModel>> GetByLcdaAsync(long lcdaId);
        Task<PageModel> GetAsync(PageModel pageModel, long lcdaId);
        Task<WardModel> GetByIdAsync(long wardId);
        Task<ResponseModel> UpdateStatusAsync(WardModel wardModel);
    }
}
