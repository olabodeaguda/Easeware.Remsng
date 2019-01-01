using Easeware.Remsng.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easeware.Remsng.Entities.Entities
{
    public class Company : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string CompanyName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string CompanyCode { get; set; }
        public long LcdaId { get; set; }
        public string Status { get; set; }
        public Lcda Lcda { get; set; }
        public ICollection<Taxpayer> Taxpayers { get; set; }
    }
}
