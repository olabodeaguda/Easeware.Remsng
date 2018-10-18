using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class WardService : IWardService
    {
        private IWardManager _wardManager;
        public WardService(IWardManager wardManager)
        {
            _wardManager = wardManager;
        }

        public Task<ResponseModel> AddAsync(WardModel wardModel)
        {
            return _wardManager.AddAsync(wardModel);
        }

        public Task<PageModel> GetAsync(PageModel pageModel, string lcdaCode)
        {
            return _wardManager.GetAsync(pageModel, lcdaCode);
        }

        public Task<WardModel> GetAsync(string wardName)
        {
            return _wardManager.GetAsync(wardName);
        }

        public Task<WardModel> GetByIdAsync(long wardId)
        {
            return _wardManager.GetByIdAsync(wardId);
        }

        public Task<List<WardModel>> GetByLcdaAsync(string lcdaCode)
        {
            return _wardManager.GetByLcdaAsync(lcdaCode);
        }

        public Task<long> LastId()
        {
            return _wardManager.LastId();
        }

        public Task<ResponseModel> UpdateAsync(WardModel wardModel)
        {
            return _wardManager.UpdateAsync(wardModel);
        }

        public Task<ResponseModel> UpdateStatusAsync(WardModel wardModel)
        {
            return _wardManager.UpdateStatusAsync(wardModel);
        }
    }
}
