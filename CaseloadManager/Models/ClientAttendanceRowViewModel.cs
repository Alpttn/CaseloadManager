using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseloadManager.Models
{
    public class ClientAttendanceRowViewModel
    {
        public Client Client { get; set; }

        public int SessionsHad { get; set; }
    }
}
