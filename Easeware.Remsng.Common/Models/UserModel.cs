using AutoMapper;
using Easeware.Remsng.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class UserModel : BaseModel
    {
        public long id { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        //public string username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string lastname { get; set; }
        public string otherNames { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is required")]
        public Gender gender { get; set; }
      
        [IgnoreMap()]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [IgnoreMap()]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Confirm password does not match password")]
        public string ConfirmPassword { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string passwordHash { get; set; }
        public string securityStamp { get; set; }
        public DateTimeOffset? lockedOutEndDateUTC { get; set; }
        public bool lockedoutenabled { get; set; }
        public int lockedoutCount { get; set; }
        public UserStatus userStatus { get; set; } = UserStatus.PENDING;

        public ICollection<UserLcdaModel> UserLcdas { get; set; }
    }
}
