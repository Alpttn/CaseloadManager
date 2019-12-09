using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class BuildingType
    {
        [Key]
        public int BuildingTypeId { get; set; } //is it id or just BuildingType

        [Required]
        [StringLength(255)]
        [Display(Name = "Facility")]
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
