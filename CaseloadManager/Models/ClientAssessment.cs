using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientAssessment
    {
        [Key]
        public int ClientAssessmentId { get; set; }
        [Required]
        [Display(Name = "Standardized Score")]
        public int StandarizedScore { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Administered")]
        public DateTime DateAdministered { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
    }
}
