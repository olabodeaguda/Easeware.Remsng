using System.ComponentModel.DataAnnotations;

namespace Easeware.Remsng.Common.Models
{
    public class TaxpayerModel : BaseModel
    {
        public long Id { get; set; }
        public string TaxCode { get; set; }

        [Required(ErrorMessage = "Company is required")]
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }

        public long AddressId { get; set; }
        public AddressModel Address { get; set; }

        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public TaxStatus Status { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public TaxCategory TaxCategory { get; set; }
    }

    public enum TaxCategory
    {
        SMALL, LARGE, COOPERATE, MEDIUM
    }

    public enum TaxStatus
    {
        ACTIVE, NOT_ACTIVE
    }
}
