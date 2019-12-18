using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientInformationEditViewModel
    {
        public Client Client { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
