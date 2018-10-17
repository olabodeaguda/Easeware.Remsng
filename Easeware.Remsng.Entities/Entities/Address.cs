using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Address : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string HouseNumber { get; set; }
        public long StreetId { get; set; }
        public long OwnerId { get; set; }

        public Street Street { get; set; }
    }
}
