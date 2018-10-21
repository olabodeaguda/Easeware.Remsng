using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class CompanyModel
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public CompanyStatus Status { get; set; }
    }
}
