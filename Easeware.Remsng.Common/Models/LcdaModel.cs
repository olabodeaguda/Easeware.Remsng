using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class LcdaModel
    {
        public LcdaModel()
        {
            Licenses = new List<RemsLicenceModel>();
        }
        public long Id { get; set; }
        public string LcdaCode { get; set; }
        [Required(ErrorMessage = "Lcda Name is required!!!")]
        public string LcdaName { get; set; }
        public List<RemsLicenceModel> Licenses { get; set; }
    }
}
