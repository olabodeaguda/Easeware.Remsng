using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class UserLcdaModel
    {
        [Required(ErrorMessage = "User is required")]
        public long UserId { get; set; }
        public UserModel User { get; set; }
        [Required(ErrorMessage = "Lcda is required")]
        public long LcdaId { get; set; }
        public LcdaModel Lcda { get; set; }
    }
}
