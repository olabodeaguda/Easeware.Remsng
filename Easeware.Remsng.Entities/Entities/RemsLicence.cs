using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class RemsLicence : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public long LcdaId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LicenseValue { get; set; }
        public Lcda Lcda { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string LicenseStatus { get; set; }

        public int UsageCount { get; set; }
    }
}
