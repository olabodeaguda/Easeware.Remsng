namespace Easeware.Remsng.Common.Models
{
    public class AddressModel
    {
        public long Id { get; set; }
        public string HouseNumber { get; set; }
        public string StreetCode { get; set; }
        public long OwnerId { get; set; }

        public StreetModel Street { get; set; }
    }
}
