using System.ComponentModel.DataAnnotations;

namespace Easeware.Remsng.Common.Models
{
    public class TaxpayerRegistrationModel
    {
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "House number is required")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public long StreetId { get; set; }

        [Required(ErrorMessage = "Tax Category is required")]
        public TaxCategory TaxCategory { get; set; }
    }
}
