using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        public int FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

    }
}
