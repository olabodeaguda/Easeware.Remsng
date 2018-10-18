using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class UserLcda
    {
        public string UserEmail { get; set; }
        public User User { get; set; }
        public string LcdaCode { get; set; }
        public Lcda Lcda { get; set; }
    }
}
