using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface ILicenceService
    {
        string Encrypt(LicenceModel licenceModel);
        LicenceModel Decrypt(string value);

    }
}
