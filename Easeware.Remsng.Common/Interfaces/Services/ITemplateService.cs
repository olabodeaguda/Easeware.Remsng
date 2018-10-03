using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ITemplateService
    {
        Task<string> GetTemplate(TemplateType templateType);
    }
}
