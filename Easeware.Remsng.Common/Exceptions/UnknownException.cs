using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message)
        {
        }
    }
}
