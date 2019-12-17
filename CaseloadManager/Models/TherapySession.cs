using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class TherapySession
    {
        [Key]
        public int TherapySessionId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(225, ErrorMessage = "Please shorten the note to 225 characters")]
        [MinLength(2, ErrorMessage = "Must type more than 1 character in text area")]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public virtual ICollection<TherapySessionGoal> TherapySessionGoals { get; set; }

    }
}
