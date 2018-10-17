using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ISectorManager
    {
        Task<ResponseModel> AddAsync(SectorModel sectorModel);
        Task<ResponseModel> UpdateAsync(SectorModel sectorModel);
        Task<List<SectorModel>> GetAllAsync();
        Task<SectorModel> Get(long Id);
    }
}
