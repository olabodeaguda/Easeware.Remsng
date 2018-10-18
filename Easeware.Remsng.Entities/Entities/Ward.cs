using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Ward : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string WardCode { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string WardName { get; set; }
        public string LcdaCode { get; set; }

        public Lcda Lcda { get; set; }
        public string Status { get; set; }

        public ICollection<Street> Streets { get; set; }
    }
}
