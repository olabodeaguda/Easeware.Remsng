using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Lcda : BaseModel
    {
        public long Id { get; set; }
        [Column(TypeName="nvarchar(20)")]
        public string LcdaCode { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LcdaName { get; set; }
        public ICollection<RemsLicence> Licenses { get; set; }
    }
}
