using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Exceptions
{
    public class TokenExpireException : Exception
    {
        public TokenExpireException(string message) : base(message)
        {
        }
    }
}
