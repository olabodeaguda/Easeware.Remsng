using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Sector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string SectorCode { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string SectorName { get; set; }
        public string LcdaCode { get; set; }
    }
}
