namespace ISEPay.DAL.Persistence.Entities
{
    public class Permission : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }
        public string ActionType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PermissionType { get; set; }

        // Navigation property for the many-to-many relationship with Role
        public ICollection<Role> Roles { get; set; }
    }
}
