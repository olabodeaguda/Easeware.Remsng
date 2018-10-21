using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class SectorModel : BaseModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Sector code is required")]
        public string SectorCode { get; set; }
        [Required(ErrorMessage = "Name code is required")]
        public string SectorName { get; set; }
        [Required(ErrorMessage = "LCDA code is required")]
        public string LcdaCode { get; set; }
        public SectorStatus SectorStatus { get; set; } = SectorStatus.ACTIVE;
    }
}
