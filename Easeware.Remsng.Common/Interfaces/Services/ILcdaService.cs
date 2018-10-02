using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ILcdaService
    {
        Task<bool> Add(LcdaModel lcdaModel);
        Task<bool> Update(LcdaModel lcdaModel);
        Task<LcdaModel> Get(long id);
        Task<LcdaModel> Get(string lcdaCode);
        Task<PageModel> Get(PageModel pageModel);
        Task<long> LastId();
    }
}
