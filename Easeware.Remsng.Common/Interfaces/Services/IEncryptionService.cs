﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}