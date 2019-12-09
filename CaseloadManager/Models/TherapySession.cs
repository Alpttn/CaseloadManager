using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class TherapySession
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public virtual ICollection<TherapySessionGoal> TherapySessionGoals { get; set; }

    }
}
