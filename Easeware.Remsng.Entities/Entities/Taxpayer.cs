using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Taxpayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CompanyCode { get; set; }
        public string AddressCode { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string OtherNames { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }

    }
}
