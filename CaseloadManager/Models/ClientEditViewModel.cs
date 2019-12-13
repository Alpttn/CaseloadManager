using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientEditViewModel
    {
        public virtual ICollection<ClientAssessment> ClientAssessments { get; set; }

        public Client Client { get; set; }
    }
}
