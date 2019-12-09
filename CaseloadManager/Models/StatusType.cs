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
        public int StatusTypeId { get; set; } //is it id or just StatusType

        [Required]
        [StringLength(255)]
        [Display(Name = "Status")]
        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
