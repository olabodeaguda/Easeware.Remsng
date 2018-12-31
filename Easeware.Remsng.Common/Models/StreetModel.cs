using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Easeware.Remsng.Common.Models
{
    public class StreetModel : BaseModel
    {
        public long Id { get; set; }

        public string StreetCode { get; set; }
        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get; set; }
        public StreetStatus StreetStatus { get; set; }
        [Required(ErrorMessage = "Ward is required")]
        public long WardId { get; set; }
        public WardModel Ward { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
    }

    public enum StreetStatus
    {
        ACTIVE, NOT_ACTIVE
    }
}
