using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class WardManager : IWardManager
    {
        private ILcdaRepository _lcdaRepo;
        private IHttpContextAccessor _httpAccessor;
        private IWardRepository _wRepo;
        private readonly ICodeGeneratorService _codeGeneratorService;
        public WardManager(IWardRepository wRepo,
            IHttpContextAccessor httpContextAccessor,
            ICodeGeneratorService codeGeneratorService, ILcdaRepository lcdaRepo)
        {
            _wRepo = wRepo;
            _httpAccessor = httpContextAccessor;
            _codeGeneratorService = codeGeneratorService;
            _lcdaRepo = lcdaRepo;
        }

        public async Task<WardModel> CreateWard(WardModel wardModel)
        {
            WardModel wModel = await Get(wardModel.WardName, wardModel.LcdaId);
            if (wModel != null)
            {
                throw new BadRequestException($"{wardModel.WardName} already exist");
            }
            long wardid = await LastId();
            wardModel.WardCode = _codeGeneratorService.NewCode(wardid, "WRD");
            wardModel.CreatedBy = _httpAccessor.HttpContext.User.Identity.Name;

            return await _wRepo.AddAsync(wardModel);
        }

        public async Task<PageModel> Get(PageModel pageModel, string lcdaCode)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Lcda is required");
            }
            return await _wRepo.GetAsync(pageModel, lcdaCode);
        }

        public async Task<WardModel> Get(long wardId)
        {
            if (wardId == default(long))
            {
                return null;
            }
            return await _wRepo.GetByIdAsync(wardId);
        }

        public async Task<WardModel> Get(string wardName)
        {
            if (string.IsNullOrEmpty(wardName))
            {
                return null;
            }
            return await _wRepo.GetAsync(wardName);
        }
        public async Task<WardModel> Get(string wardName, long lcdaId)
        {
            if (string.IsNullOrEmpty(wardName))
            {
                return null;
            }
            else if (lcdaId == default(long))
            {
                return null;
            }
            return await _wRepo.GetAsync(wardName, lcdaId);
        }

        public async Task<List<WardModel>> GetByLcdaWard(string lcdaCode)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                return null;
            }
            var res = await _wRepo.GetByLcdaAsync(lcdaCode);
            return res.Where(x => x.Status == WardStatus.ACTIVE).ToList();
        }

        public async Task<long> LastId()
        {
            return await _wRepo.LastId();
        }

        public async Task<WardModel> UpdateStatusAsync(WardModel wardModel)
        {
            wardModel.ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name;
            return await _wRepo.UpdateStatusAsync(wardModel);
        }

        public async Task<WardModel> UpdateWard(WardModel wardModel)
        {
            wardModel.ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name;
            if (wardModel.Id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wRepo.GetByIdAsync(wardModel.Id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward can not be found");
            }

            if (wModel.Status == WardStatus.NOT_ACTIVE)
            {
                throw new BadRequestException("Ward can not be updated because status is not active");
            }

            return await _wRepo.UpdateAsync(wardModel);
        }

        public async Task<WardModel> DeActivate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wRepo.GetByIdAsync(id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward does not exist");
            }

            if (wModel.Status == WardStatus.NOT_ACTIVE)
            {
                throw new BadRequestException("Ward is already not active");
            }

            wModel.Id = id;
            wModel.Status = WardStatus.NOT_ACTIVE;
            return await _wRepo.UpdateStatusAsync(wModel);
        }

        public async Task<WardModel> Activate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wRepo.GetByIdAsync(id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward does not exist");
            }

            if (wModel.Status == WardStatus.NOT_ACTIVE)
            {
                throw new BadRequestException("Ward is already not active");
            }

            wModel.Id = id;
            wModel.Status = WardStatus.ACTIVE;
            return await _wRepo.UpdateStatusAsync(wModel);
        }
    }
}
