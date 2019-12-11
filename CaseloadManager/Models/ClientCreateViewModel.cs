using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CaseloadManager.Models
{
    public class ClientCreateViewModel
    {
        public Client Client { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
        
    }
}
