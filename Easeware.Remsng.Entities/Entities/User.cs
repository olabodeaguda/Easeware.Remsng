using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class User : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string lastname { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string otherNames { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string gender { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string passwordHash { get; set; }
        public string securityStamp { get; set; }
        public DateTimeOffset? lockedOutEndDateUTC { get; set; }
        public bool lockedoutenabled { get; set; }
        public int lockedoutCount { get; set; }
        public string userStatus { get; set; }

        public ICollection<UserLcda> UserLcdas { get; set; }

       
    }
}
