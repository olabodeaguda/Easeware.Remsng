using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ISectorService
    {
        Task<ResponseModel> AddAsync(SectorModel sectorModel);
        Task<ResponseModel> UpdateAsync(SectorModel sectorModel);
        Task<List<SectorModel>> GetByLcdaAsync(string lcdaCode);
        Task<SectorModel> Get(long Id);
    }
}
