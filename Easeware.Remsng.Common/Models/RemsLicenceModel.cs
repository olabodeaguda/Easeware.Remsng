using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class RemsLicenceModel
    {
        public long Id { get; set; }
        public long LcdaId { get; set; }
        public string LicenseValue { get; set; }
        public LcdaModel Lcda { get; set; }
        public string LicenseStatus { get; set; }
        public int UsageCount { get; set; }
    }
}
