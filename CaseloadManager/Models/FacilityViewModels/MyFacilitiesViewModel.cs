using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models.FacilityViewModels
{
    public class MyFacilitiesViewModel
    {
        
        //public int FacilityId { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Facility Address")]
        public string Adress { get; set; }

        public int FacilityTypeId { get; set; }

        public FacilityType FacilityType { get; set; }

        public Facility Facility { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public IEnumerable<Facility> Facilities { get; set; }

    }
}
