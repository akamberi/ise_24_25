using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.Common.Models
{
    public class AddLineViewModel
    {
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
        public decimal Price { get; set; }
        // public DateTime Date { get; set; }
    }
}
