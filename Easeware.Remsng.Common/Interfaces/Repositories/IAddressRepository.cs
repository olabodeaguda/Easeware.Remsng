using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<AddressModel> CreateAddress(AddressModel model);
        Task<AddressModel> UpdateAddress(AddressModel model);
        Task<AddressModel> UpdateAddressStatus(AddressModel model);
        Task<AddressModel> Get(long id);
        Task<List<AddressModel>> GetByLcda(long lcdaId);
    }
}
