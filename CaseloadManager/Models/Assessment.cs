using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Assessment
    {
        [Required]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        [Required]
        [Display(Name = "Oldest Allowed")]
        public int OldestAllowed { get; set; }

        [Required]
        [Display(Name = "Youngest Allowed")]
        public int YoungestAllowed { get; set; }

        public virtual ICollection<ClientAssessment> ClientAssessmets { get; set; }
        
    }
}
