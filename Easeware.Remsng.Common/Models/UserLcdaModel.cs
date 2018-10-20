using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class UserLcdaModel
    {
        [Required(ErrorMessage = "User is required")]
        public string UserEmail { get; set; }
        public UserModel User { get; set; }
        [Required(ErrorMessage = "Lcda is required")]
        public string LcdaCode { get; set; }
        public LcdaModel Lcda { get; set; }
        public UserLcdaStatus Status { get; set; }
    }
}
