using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Lcda : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string LcdaCode { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LcdaName { get; set; }
        public string LcdaStatus { get; set; }
        public long StateId { get; set; }

        public State State { get; set; }
        public ICollection<RemsLicence> Licenses { get; set; }
        public ICollection<UserLcda> UserLcdas { get; set; }
        public ICollection<Ward> Wards { get; set; }
    }
}
