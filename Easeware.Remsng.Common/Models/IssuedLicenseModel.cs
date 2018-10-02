using Easeware.Remsng.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class IssuedLicenseModel
    {
        public long Id { get; set; }
        public string Lcda { get; set; }
        public string LicenseValue { get; set; }
        public bool Isvalid
        {
            get { return LicenseValue.ValidateLicense(); }
        }
    }
}
