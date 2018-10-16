using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ICodeGeneratorService
    {
        string NewCode(long initiialId, string pre);
        string VerificationCode();
    }
}
