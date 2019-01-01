using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class CompanyManager : ICompanyManager
    {
        private IHttpContextAccessor _httpAccessor;
        private readonly ICompanyRepository _cRepo;
        private readonly ICodeGeneratorService _codeGeneratorService;
        public CompanyManager(ICompanyRepository cRepo,
            IHttpContextAccessor httpContextAccessor,
            ICodeGeneratorService codeGeneratorService)
        {
            _cRepo = cRepo;
            _httpAccessor = httpContextAccessor;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<CompanyModel> Activate(long Id)
        {
            if (Id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            return await _cRepo.UpdateStatusAsync(new CompanyModel()
            {
                Id = Id,
                Status = Common.Enums.CompanyStatus.ACTIVE,
                ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name
            });
        }

        public async Task<CompanyModel> Add(CompanyModel companyModel)
        {
            long companyId = await _cRepo.LastId();
            companyModel.CompanyCode = _codeGeneratorService.NewCode(companyId, "CMP");
            companyModel.CreatedBy = _httpAccessor.HttpContext.User.Identity.Name;
            return await _cRepo.AddSync(companyModel);
        }

        public async Task<CompanyModel> Deactivate(long Id)
        {
            if (Id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }
            return await _cRepo.UpdateStatusAsync(new CompanyModel()
            {
                Id = Id,
                Status = Common.Enums.CompanyStatus.NOT_ACTIVE,
                ModifiedBy = _httpAccessor.HttpContext.User.Identity.Name
            });
        }

        public async Task<List<CompanyModel>> Get(string lcdaCode)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _cRepo.GetAsync(lcdaCode);
        }

        public async Task<PageModel> Get(string lcdaCode, PageModel pageModel)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _cRepo.GetAsync(lcdaCode, pageModel);
        }

        public async Task<CompanyModel> Get(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _cRepo.GetAsync(id);
        }

        public async Task<long> LastId()
        {
            return await _cRepo.LastId();
        }

        public async Task<CompanyModel> Update(CompanyModel companyModel)
        {
            return await _cRepo.UpdateAsync(companyModel);
        }
    }
}
