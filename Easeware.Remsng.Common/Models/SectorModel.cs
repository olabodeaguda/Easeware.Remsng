using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class SectorModel
    {
        public long Id { get; set; }
        public string SectorCode { get; set; }
        public string SectorName { get; set; }
        public long LcdaId { get; set; }
    }
}
