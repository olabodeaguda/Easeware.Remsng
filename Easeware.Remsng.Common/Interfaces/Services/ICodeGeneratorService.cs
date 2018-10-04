using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ICodeGeneratorService
    {
        string NewLcdaCode(long initiialId);
        string VerificationCode();
    }
}
