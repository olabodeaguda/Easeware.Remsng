using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class LoginResponseModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string fullName { get; set; }
        public long accessId { get; set; }
    }
}
