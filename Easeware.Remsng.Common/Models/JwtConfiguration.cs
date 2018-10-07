using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public double TokenLifespan { get; set; }
        public double SessionLifespan { get; set; }
        public string RsaPrivateKeyXml { get; set; }
        public string RsaPublicKeyXml { get; set; }
    }
}
