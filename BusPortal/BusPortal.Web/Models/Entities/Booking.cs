namespace BusPortal.Web.Models.Entities
{
    public class Booking
    {
        public Guid ClientId { get; set; }
        public Guid LineId { get; set; }
        public Client Client { get; set; }
        public Line Line { get; set; }
        public DateTime DateTime { get; set; }
        public string Seat { get; set; }
        public decimal Price { get; set; }

    }
}
