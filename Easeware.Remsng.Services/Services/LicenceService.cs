using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Easeware.Remsng.Infrastructure.Services
{
    public class LicenceService : ILicenceService
    {
        private readonly IEncryptionService _encryptionService;
        public LicenceService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }
        public LicenceModel Decrypt(string value)
        {
            string[] dd = value.Split(new char[] { '-' });
            if (dd.Length != 3 && dd[0] != dd[2].FromHexString())
            {
                return null;
            }

            long ticks;
            if (!long.TryParse(dd[1], out ticks))
            {
                return null;
            }

            return new LicenceModel()
            {
                ValidTimeSpan = ticks,
                LcdaCode = dd[0],
                DateString = new DateTime(ticks).ToString("dd-MM-yyyy")
            };
        }

        public string Encrypt(LicenceModel licenceModel)
        {
            string hex_lcdaCode = licenceModel.LcdaCode.ToHexString();
            string value = $"{licenceModel.LcdaCode}-{licenceModel.ValidTimeSpan}-{hex_lcdaCode}";
            return value;
        }
    }
}
