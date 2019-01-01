using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Easeware.Remsng.Common.Models
{
    public class AddressModel : BaseModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "House number is required")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public long StreetId { get; set; }
        public AddressStatus Status { get; set; } = AddressStatus.ACTIVE;

        public StreetModel Street { get; set; }
        public ICollection<TaxpayerModel> Taxpayers { get; set; }
    }

    public enum AddressStatus
    {
        ACTIVE, NOT_ACTIVE
    }
}
