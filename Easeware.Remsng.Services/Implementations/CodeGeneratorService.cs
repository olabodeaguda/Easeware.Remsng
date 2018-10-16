using Easeware.Remsng.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Services.Implementations
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        public string NewCode(long initiialId, string pre)
        {
            string idString = (initiialId + 1) > 9 ? (initiialId + 1).ToString() : "0" + (initiialId + 1);
            return $"{pre}-{idString}";
        }

        public string VerificationCode()
        {
            DateTime dateTime = DateTime.Now;
            return $"{dateTime.Year}{dateTime.DayOfYear}{dateTime.ToString("HHmmssfff")}";
        }
    }
}
