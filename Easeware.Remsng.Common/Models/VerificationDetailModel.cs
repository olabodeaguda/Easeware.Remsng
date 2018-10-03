using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class VerificationDetailModel : BaseModel
    {
        public long Id { get; set; }
        public string VerificationCode { get; set; }
        public string OwnerId { get; set; }
        public bool IsVerified { get; set; }

    }
}
