using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientAttendanceRowViewModel
    {
        public Client Client { get; set; }
        [Display(Name = "Sessions Complete")]
        public int SessionsHad { get; set; }
    }
}
