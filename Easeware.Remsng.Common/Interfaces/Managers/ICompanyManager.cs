using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ICompanyManager
    {
        Task<CompanyModel> Add(CompanyModel companyModel);
        Task<CompanyModel> Update(CompanyModel companyModel);
        Task<List<CompanyModel>> Get(string lcdaCode);
        Task<PageModel> Get(string lcdaCode, PageModel pageModel);
        Task<CompanyModel> Get(long id);
        Task<CompanyModel> Deactivate(long Id);
        Task<CompanyModel> Activate(long Id);
        Task<long> LastId();
    }
}
