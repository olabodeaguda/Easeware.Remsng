using Easeware.Remsng.Common.Interfaces.Services;
using System;

namespace Easeware.Remsng.Infrastructure.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        public string NewCode(long initiialId, string pre)
        {
            string idString = (initiialId + 1) > 9 ? (initiialId + 1).ToString() : "0" + (initiialId + 1);
            string mill = DateTime.Now.ToString("ss");
            return $"{pre}-{idString}{mill}";
        }

        public string VerificationCode()
        {
            DateTime dateTime = DateTime.Now;
            return $"{dateTime.Year}{dateTime.DayOfYear}{dateTime.ToString("HHmmssfff")}";
        }
    }
}
