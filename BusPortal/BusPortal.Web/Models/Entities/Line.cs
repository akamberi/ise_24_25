using Microsoft.EntityFrameworkCore;

namespace BusPortal.Web.Models.Entities
{
    public class Line
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }


    }
}
