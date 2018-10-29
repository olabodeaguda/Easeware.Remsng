using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<ResponseModel> AddAsync(CompanyModel companyModel);
        Task<ResponseModel> UpdateAsyc(CompanyModel companyModel);
        Task<List<CompanyModel>> GetAsync(string lcdaCode);
        Task<CompanyModel> GetAsync(long companyId);
        Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel);
    }
}
