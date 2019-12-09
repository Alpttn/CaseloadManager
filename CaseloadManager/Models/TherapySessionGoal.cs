using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class TherapySessionGoal
    {
        [Required]
        public int GoalId { get; set; }

        [Required]
        public int TherapySessionId { get; set; }

        public Goal Goal { get; set; }
        //public TherapySession TherapySession { get; set; }
    }
}
