using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class WardModel : BaseModel
    {
        public long Id { get; set; }

        public string WardCode { get; set; }

        [Required(ErrorMessage = "Ward Name is required")]
        public string WardName { get; set; }

        [Required(ErrorMessage = "Lcda is required")]
        public string LcdaId { get; set; }
        public WardStatus Status { get; set; }
    }
}
