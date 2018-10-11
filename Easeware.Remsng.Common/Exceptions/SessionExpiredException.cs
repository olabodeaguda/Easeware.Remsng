using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Exceptions
{
    public class SessionExpiredException : Exception
    {
        public SessionExpiredException(string message) : base(message)
        {
        }
    }
}
