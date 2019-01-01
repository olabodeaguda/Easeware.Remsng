using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IAddressManager
    {
        Task<AddressModel> CreateAddress(AddressModel model);
        Task<AddressModel> UpdateAddress(AddressModel model);
        Task<AddressModel> Activate(long id);
        Task<AddressModel> DeActivate(long id);
        Task<AddressModel> Get(long id);
        Task<List<AddressModel>> GetByLcda(long lcdaId);
    }
}
