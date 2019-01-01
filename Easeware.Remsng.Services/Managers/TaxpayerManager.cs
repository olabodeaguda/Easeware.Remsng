using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Repositories;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class TaxpayerManager : ITaxpayerManager
    {
        private IAddressRepository _addRepo;
        private IHttpContextAccessor _httpContextAccessor;
        private ITaxpayerRepository _tpRepo;
        private readonly ICodeGeneratorService _codeGeneratorService;
        public TaxpayerManager(ITaxpayerRepository taxpayerRepository,
            IHttpContextAccessor httpContextAccessor,
            ICodeGeneratorService codeGeneratorService,
            IAddressRepository addressRepository)
        {
            _tpRepo = taxpayerRepository;
            _httpContextAccessor = httpContextAccessor;
            _codeGeneratorService = codeGeneratorService;
            _addRepo = addressRepository;
        }
        public async Task<TaxpayerModel> CreateTaxpayer(TaxpayerRegistrationModel model)
        {
            AddressModel addressModel = new AddressModel()
            {
                CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                HouseNumber = model.HouseNumber,
                StreetId = model.StreetId,
            };
            addressModel = await _addRepo.CreateAddress(addressModel);
            long intialId = await _tpRepo.LastId();
            TaxpayerModel taxpayerModel = new TaxpayerModel()
            {
                AddressId = addressModel.Id,
                CompanyId = model.CompanyId,
                CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                LastName = model.LastName,
                OtherNames = model.OtherNames,
                TaxCategory = model.TaxCategory,
                TaxCode = _codeGeneratorService.NewCode(intialId, "TX")
            };
            taxpayerModel = await _tpRepo.CreateTaxpayer(taxpayerModel);
            return taxpayerModel;
        }

        public async Task<PageModel> Get(long lcdaId, PageModel pageModel)
        {
            if (lcdaId == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            return await _tpRepo.Get(lcdaId, pageModel);
        }

        public async Task<List<TaxpayerModel>> GetAsync(long streetId)
        {
            if (streetId == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            return await _tpRepo.GetAsync(streetId);
        }

        public async Task<List<TaxpayerModel>> GetByLcda(long lcdaId)
        {
            if (lcdaId == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            return await _tpRepo.GetByLcda(lcdaId);
        }

        public async Task<TaxpayerModel> Activate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _tpRepo.UpdateStatus(new TaxpayerModel()
            {
                Id = id,
                Status = TaxStatus.ACTIVE,
                ModifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                ModifiedDate = DateTimeOffset.Now
            });
        }

        public async Task<TaxpayerModel> Deactivate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _tpRepo.UpdateStatus(new TaxpayerModel()
            {
                Id = id,
                Status = TaxStatus.NOT_ACTIVE,
                ModifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                ModifiedDate = DateTimeOffset.Now
            });
        }

        public async Task<TaxpayerModel> UpdateTaxpayer(TaxpayerModel model)
        {
            model.ModifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            model.ModifiedDate = DateTimeOffset.Now;
            return await _tpRepo.UpdateTaxpayer(model);
        }
    }
}
