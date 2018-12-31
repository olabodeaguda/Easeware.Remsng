using Easeware.Remsng.Common.Enums;

namespace Easeware.Remsng.Common.Models
{
    public class UserLcdaModel
    {
        public UserModel User { get; set; }
        public LcdaModel Lcda { get; set; }
        public UserLcdaStatus Status { get; set; } = UserLcdaStatus.ACTIVE;
        public long UserId { get; set; }
        public long LcdaId { get; set; }
    }
}
