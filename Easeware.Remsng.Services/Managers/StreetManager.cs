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
    public class StreetManager : IStreetManager
    {
        private readonly IWardManager _wManager;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IStreetRepository _sRepo;
        private readonly ICodeGeneratorService _codeGenerationService;
        public StreetManager(IStreetRepository sRepo, IHttpContextAccessor httpContextAccessor,
            ICodeGeneratorService codeGenerationService, IWardManager wardManager)
        {
            _sRepo = sRepo;
            _httpAccessor = httpContextAccessor;
            _codeGenerationService = codeGenerationService;
            _wManager = wardManager;
        }

        public async Task<StreetModel> CreateStreet(StreetModel streetModel)
        {
            WardModel wm = await _wManager.Get(streetModel.WardId);
            if (wm == null)
            {
                throw new BadRequestException("Ward does not exist");
            }
            long initial = await _sRepo.LastId();
            streetModel.StreetCode = _codeGenerationService.NewCode(initial, "STR");
            StreetModel sm = await _sRepo.CreateStreet(streetModel);
            return sm;
        }

        public async Task<List<StreetModel>> Get(long wardId)
        {
            if (wardId == default(long))
            {
                throw new BadRequestException("Ward ID is required");
            }
            return await _sRepo.Get(wardId);
        }

        public async Task<List<StreetModel>> Get(string wardCode)
        {
            if (string.IsNullOrEmpty(wardCode))
            {
                throw new BadRequestException("Ward Code is required");
            }
            return await _sRepo.Get(wardCode);
        }

        public async Task<PageModel> Get(PageModel pageModel, long wardId)
        {
            if (wardId == default(long))
            {
                throw new BadRequestException("Ward ID is required");
            }
            return await _sRepo.Get(pageModel, wardId);
        }

        public async Task<StreetModel> UpdateStreet(StreetModel streetModel)
        {
            streetModel.ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name;
            streetModel.ModifiedDate = DateTimeOffset.Now;
            StreetModel st = await _sRepo.UpdateStreet(streetModel);
            return st;
        }

        public async Task<StreetModel> UpdateStreetStatus(StreetModel streetModel)
        {
            streetModel.ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name;
            streetModel.ModifiedDate = DateTimeOffset.Now;
            StreetModel st = await _sRepo.UpdateStreetStatus(streetModel);
            return st;
        }
    }
}
