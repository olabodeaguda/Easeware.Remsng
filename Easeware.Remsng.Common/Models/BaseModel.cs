using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class BaseModel
    {
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        [Column(TypeName = "nvarchar(100)")]
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ModifiedBy { get; set; }
    }
}
