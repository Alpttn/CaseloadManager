using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientEditViewModel
    {
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public Client Client { get; set; }
    }
}
