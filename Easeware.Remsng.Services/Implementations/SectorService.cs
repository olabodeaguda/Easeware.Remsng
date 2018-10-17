using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class SectorService : ISectorService
    {
        private ISectorManager _sectorManager;
        public SectorService(ISectorManager sectorManager)
        {
            _sectorManager = sectorManager;
        }
        public Task<ResponseModel> AddAsync(SectorModel sectorModel)
        {
            return _sectorManager.AddAsync(sectorModel);
        }

        public Task<SectorModel> Get(long Id)
        {
            return _sectorManager.Get(Id);
        }

        public Task<List<SectorModel>> GetAllAsync()
        {
            return _sectorManager.GetAllAsync();
        }

        public Task<ResponseModel> UpdateAsync(SectorModel sectorModel)
        {
            return _sectorManager.UpdateAsync(sectorModel);
        }
    }
}
