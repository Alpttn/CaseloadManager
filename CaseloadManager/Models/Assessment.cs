using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Assessment
    {
        [Key]
        public int AssessmentId { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Title cannot be more than 40 characters")]
        [MinLength(2, ErrorMessage = "Must type more than 1 character in text area")]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        [Display(Name = "Oldest Allowed")]
        public int OldestAllowed { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        [Display(Name = "Youngest Allowed")]
        public int YoungestAllowed { get; set; }

        public virtual ICollection<ClientAssessment> ClientAssessmets { get; set; }
        
    }
}
