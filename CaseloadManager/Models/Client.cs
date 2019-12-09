using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Client
    {
        [Required]
        [Display(Name = "Initial")]
        public string FirstInitial { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }
        
       
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        
        [Display(Name = "Sessions Per Week")]
        public int SessionsPerWeek { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int FacilityId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ICollection<TherapySession> TherapySessions { get; set; }
        public virtual ICollection<Assessment> Assessmets { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
