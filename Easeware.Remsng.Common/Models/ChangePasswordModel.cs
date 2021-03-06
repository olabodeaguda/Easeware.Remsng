﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class ChangePasswordModel
    {
        //[Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }
        [StringLength(20, ErrorMessage = "Password length must be more than 8", MinimumLength = 8)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm password does not match new password")]
        public string ConfirmNewPassword { get; set; }
    }
}
