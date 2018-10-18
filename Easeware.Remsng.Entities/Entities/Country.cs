using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string CountryCode { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
    }
}
