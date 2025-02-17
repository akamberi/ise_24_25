namespace BusPortal.DAL.Persistence.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int amount { get; set; }
        public DateTime created_at { get; set; }
    }
}
