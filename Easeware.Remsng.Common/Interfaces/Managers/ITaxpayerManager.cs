using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ITaxpayerManager
    {
        Task<TaxpayerModel> CreateTaxpayer(TaxpayerRegistrationModel model);
        Task<List<TaxpayerModel>> GetAsync(long streetId);
        Task<List<TaxpayerModel>> GetByLcda(long lcdaId);
        Task<PageModel> Get(long lcdaId, PageModel pageModel);
        Task<TaxpayerModel> UpdateTaxpayer(TaxpayerModel model);
        Task<TaxpayerModel> Activate(long id);
        Task<TaxpayerModel> Deactivate(long id);

    }
}
