using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class LcdaService : ILcdaService
    {
        private ILcdaManager _lcdaManager;
        public LcdaService(ILcdaManager lcdaManager)
        {
            _lcdaManager = lcdaManager;
        }

        public Task<bool> Add(LcdaModel lcdaModel)
        {
            return _lcdaManager.Add(lcdaModel);
        }

        public Task<LcdaModel> Get(long id)
        {
            return _lcdaManager.Get(id);
        }

        public Task<LcdaModel> Get(string lcdaCode)
        {
            return _lcdaManager.Get(lcdaCode);
        }

        public Task<PageModel> Get(PageModel pageModel)
        {
            return _lcdaManager.Get(pageModel);
        }

        public Task<long> LastId()
        {
            return _lcdaManager.LastId();
        }

        public Task<bool> Update(LcdaModel lcdaModel)
        {
            return _lcdaManager.Update(lcdaModel);
        }
    }
}
