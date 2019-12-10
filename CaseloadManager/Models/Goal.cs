using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public virtual ICollection<TherapySessionGoal> TherapySessionGoals { get; set; }
    }
}
