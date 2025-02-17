namespace BusPortal.DAL.Persistence.Entities
{
    public class Client : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public string Password { get; set; }
    }
}
