namespace BusPortal.Web.Models.DTO
{
    public class UpdateLineViewModel
    {
        public Guid Id { get; set; }
        public string StartCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTimes { get; set; }
    }

}
