using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class FacilityType
    {
        [Key]
        public int FacilityTypeId { get; set; } 

        [Required]
        [Display(Name = "Facility")]
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
