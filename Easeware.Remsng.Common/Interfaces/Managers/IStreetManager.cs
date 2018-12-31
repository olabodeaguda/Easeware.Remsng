using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IStreetManager
    {
        Task<StreetModel> CreateStreet(StreetModel streetModel);
        Task<StreetModel> UpdateStreet(StreetModel streetModel);
        Task<StreetModel> UpdateStreetStatus(StreetModel streetModel);
        Task<List<StreetModel>> Get(long wardId);
        Task<List<StreetModel>> Get(string wardCode);
        Task<PageModel> Get(PageModel pageModel, long wardId);
    }
}
