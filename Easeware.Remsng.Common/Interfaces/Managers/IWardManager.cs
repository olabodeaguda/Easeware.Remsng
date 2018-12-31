using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IWardManager
    {
        Task<WardModel> CreateWard(WardModel wardModel);
        Task<WardModel> UpdateWard(WardModel wardModel);
        Task<List<WardModel>> GetByLcdaWard(string lcdaCode);
        Task<PageModel> Get(PageModel pageModel, string lcdaCode);
        Task<WardModel> Get(long wardId);
        Task<WardModel> UpdateStatusAsync(WardModel wardModel);
        Task<WardModel> Get(string wardName);
        Task<long> LastId();
        Task<WardModel> DeActivate(long id);
        Task<WardModel> Activate(long id);
    }
}
