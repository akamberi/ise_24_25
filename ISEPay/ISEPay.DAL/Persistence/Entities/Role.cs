using System.Security;

namespace ISEPay.DAL.Persistence.Entities
{

    // nje user ne sistem ka disa role {ADMIN, AGENT, USER} si dhe te dhena te nevojshme per seciien
    public class Role : BaseEntity<Guid>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
