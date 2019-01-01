using Easeware.Remsng.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easeware.Remsng.Entities.Entities
{
    public class Taxpayer : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string TaxCode { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string OtherNames { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }
        public string TaxCategory { get; set; }
    }
}
