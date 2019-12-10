using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Initial")]
        public string FirstInitial { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstInitial}. {LastName}";
            }
        }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [RegularExpression(@"^[a-zA-Z  0-9]*$", ErrorMessage = "No special characters.")]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        
        [Display(Name = "Sessions Per Week")]
        public int? SessionsPerWeek { get; set; }

        [Required]
        public int StatusTypeId { get; set; }
        public StatusType StatusType { get; set; }

        [Required]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ICollection<TherapySession> TherapySessions { get; set; }
        public virtual ICollection<ClientAssessment> ClientAssessmets { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        
    }
}
