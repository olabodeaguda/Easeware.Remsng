using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Repositories;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class AddressManager : IAddressManager
    {
        private IHttpContextAccessor _httpAccessor;
        private IAddressRepository _adRepo;
        public AddressManager(IAddressRepository adRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _adRepo = adRepo;
            _httpAccessor = httpContextAccessor;
        }

        public async Task<AddressModel> Activate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            AddressModel addressModel = new AddressModel()
            {
                Id = id,
                Status = AddressStatus.ACTIVE,
                ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name,
                ModifiedDate = DateTimeOffset.Now
            };
            return await _adRepo.UpdateAddressStatus(addressModel);
        }

        public async Task<AddressModel> CreateAddress(AddressModel model)
        {
            model.CreatedBy = _httpAccessor.HttpContext.User.Identity.Name;

            return await _adRepo.CreateAddress(model);
        }

        public async Task<AddressModel> DeActivate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            AddressModel addressModel = new AddressModel()
            {
                Id = id,
                Status = AddressStatus.NOT_ACTIVE,
                ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name,
                ModifiedDate = DateTimeOffset.Now
            };
            return await _adRepo.UpdateAddressStatus(addressModel);
        }

        public async Task<AddressModel> Get(long id)
        {
            var result = await _adRepo.Get(id);
            if (result == null)
            {
                throw new NotFoundException("Address does not exist");
            }

            return result;
        }

        public async Task<List<AddressModel>> GetByLcda(long lcdaId)
        {
            if (lcdaId == default(long))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _adRepo.GetByLcda(lcdaId);
        }

        public async Task<AddressModel> UpdateAddress(AddressModel model)
        {
            model.ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name;
            model.ModifiedDate = DateTime.Now;
            return await _adRepo.UpdateAddress(model);
        }
    }
}
