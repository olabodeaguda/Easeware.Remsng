using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ISectorRepository
    {
        Task<int> AddAsync(SectorModel sectorModel);
        Task<ResponseModel> UpdateAsync(SectorModel sectorModel);
        Task<List<SectorModel>> Get(string lcdaCode);
        Task<SectorModel> Get(string lcdaCode, string sectorCode);
        Task<SectorModel> Get(long Id);
        Task<int> Delete(SectorModel sectorModel);
    }
}
