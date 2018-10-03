using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class UserLcda
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long LcdaId { get; set; }
        public Lcda Lcda { get; set; }
    }
}
