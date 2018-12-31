using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        public async Task<string> GetTemplate(TemplateType templateType)
        {
            if (templateType == TemplateType.EMAIL_CONFIRMATION)
            {
                var pth = Path.Combine(AppContext.BaseDirectory, "Templates/emailconfirmation.html");
                return await System.IO.File.ReadAllTextAsync(pth);
            }
            else
            if (templateType == TemplateType.PASSWORD_RESET)
            {
                var pth = Path.Combine(AppContext.BaseDirectory, "Templates/changepassword.html");
                return await System.IO.File.ReadAllTextAsync(pth);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
