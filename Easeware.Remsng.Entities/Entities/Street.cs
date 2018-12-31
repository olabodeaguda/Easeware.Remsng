using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Street : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string StreetCode { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string StreetName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string StreetStatus { get; set; }
        public long WardId { get; set; }
        public Ward Ward { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
