using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class WardModel : BaseModel
    {
        public long Id { get; set; }
        public string WardCode { get; set; }

        public string WardName { get; set; }

        public string LcdaId { get; set; }
    }
}
