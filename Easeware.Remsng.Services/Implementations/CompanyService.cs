using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private ICodeGeneratorService _codeGeneratorService;
        private ICompanyManager _companyManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CompanyService(ICompanyManager companyManager,
            IHttpContextAccessor contextAccessor,
            ICodeGeneratorService codeGeneratorService)
        {
            _companyManager = companyManager;
            _contextAccessor = contextAccessor;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<ResponseModel> AddAsync(CompanyModel companyModel)
        {
            companyModel.CompanyCode = _codeGeneratorService.NewCode(await _companyManager.LastId(), "CYB");
            companyModel.CreatedBy = _contextAccessor.HttpContext.User.Identity.Name;
            companyModel.CreatedDate = DateTimeOffset.Now;
            int count = await _companyManager.AddSync(companyModel);
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    description = $"{companyModel.CompanyName} has been added successfully"
                };
            }
            else
            {
                return new ResponseModel()
                {
                    code = ResponseCode.FAIL,
                    description = $"{companyModel.CompanyName} encounter an error. Please try again or contact an administrator"
                };
            }
        }

        public async Task<List<CompanyModel>> GetAsync(string lcdaCode)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Lcda is required");
            }

            return await _companyManager.GetAsync(lcdaCode);
        }

        public async Task<CompanyModel> GetAsync(long companyId)
        {
            if (companyId == default(long))
            {
                throw new BadRequestException("Invalid request");
            }

            return await _companyManager.GetAsync(companyId);
        }

        public Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Lcda is required");
            }
            return null;
        }

        public Task<ResponseModel> UpdateAsyc(CompanyModel companyModel)
        {
            throw new NotImplementedException();
        }
    }
}
