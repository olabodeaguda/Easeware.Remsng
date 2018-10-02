using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string message) : base(message)
        {
        }
    }
}
