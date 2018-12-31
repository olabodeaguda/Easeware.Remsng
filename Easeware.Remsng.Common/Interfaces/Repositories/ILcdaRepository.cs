using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ILcdaRepository
    {
        Task<LcdaModel> Add(LcdaModel lcdaModel);
        Task<LcdaModel> Update(LcdaModel lcdaModel);
        Task<LcdaModel> Get(long id);
        Task<LcdaModel> Get(string lcdaCode);
        Task<PageModel> Get(PageModel pageModel);
        Task<LcdaModel> GetByName(string lcdaName);
        Task<long> LastId();
    }
}
