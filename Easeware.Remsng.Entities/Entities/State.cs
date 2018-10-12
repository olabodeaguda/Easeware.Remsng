using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Easeware.Remsng.Entities.Entities
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long countryId { get; set; }
        public string stateCode { get; set; }
        public string stateName { get; set; }
        public Country Country { get; set; }

        public ICollection<Lcda> Lcdas { get; set; }
    }
}
