using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientEditStatusViewModel
    {
        public Client Client { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
