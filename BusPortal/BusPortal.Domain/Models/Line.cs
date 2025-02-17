using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.Domain.Models
{
    public class Line
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
        public int Price { get; set; }
    }
}
