using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ActivityLog
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
