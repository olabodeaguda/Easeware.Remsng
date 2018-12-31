using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ICompanyRepository
    {
        Task<CompanyModel> AddSync(CompanyModel companyModel);
        Task<CompanyModel> UpdateAsync(CompanyModel companyModel);
        Task<List<CompanyModel>> GetAsync(string lcdaCode);
        Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel);
        Task<CompanyModel> GetAsync(long id);
        Task<CompanyModel> UpdateStatusAsync(CompanyModel companyModel);
        Task<long> LastId();
    }
}
