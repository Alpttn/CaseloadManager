using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class StatusType
    {
        [Key]
        [Display(Name = "Status")]
        public int StatusTypeId { get; set; } 

        [Required]
        [Display(Name = "Status")]
        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
