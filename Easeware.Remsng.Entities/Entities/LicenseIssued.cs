using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class IssuedLicense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Lcda { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string LicenseValue { get; set; }
    }
}
