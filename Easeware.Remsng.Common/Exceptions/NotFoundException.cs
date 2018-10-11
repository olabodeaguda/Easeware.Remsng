using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
