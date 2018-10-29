using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ICompanyManager
    {
        Task<int> AddSync(CompanyModel companyModel);
        Task<int> UpdateAsync(CompanyModel companyModel);
        Task<List<CompanyModel>> GetAsync(string lcdaCode);
        Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel);
        Task<CompanyModel> GetAsync(long id);
        Task<int> UpdateStatusAsync(CompanyModel companyModel);
        Task<long> LastId();
    }
}
