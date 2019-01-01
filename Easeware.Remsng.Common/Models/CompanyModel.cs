using Easeware.Remsng.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Easeware.Remsng.Common.Models
{
    public class CompanyModel : BaseModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "LCDA is required")]
        public long LcdaId { get; set; }
        public CompanyStatus Status { get; set; } = CompanyStatus.ACTIVE;
    }
}
