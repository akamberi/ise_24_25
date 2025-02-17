using NuGet.Packaging.Signing;
namespace BusPortal.DAL.Persistence.Entities
{
    public class Payment : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public int amount { get; set; }
        public DateTime created_at { get; set; }
    }
}
