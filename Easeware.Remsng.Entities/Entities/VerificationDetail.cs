using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class VerificationDetail : BaseModel
    {
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string VerificationCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string OwnerId { get; set; }
        public bool IsVerified { get; set; }
    }
}
