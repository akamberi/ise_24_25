using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.Common.Models
{
    public class UpdateLineViewModel
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
    }

}
