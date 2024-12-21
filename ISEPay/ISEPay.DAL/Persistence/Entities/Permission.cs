namespace ISEPay.DAL.Persistence.Entities
{

    // nje user ka  permissions e caktuar shembull:
    //Name: "Create User"
    //Description: "Allow the creation of new users"
    //Resource: "User"
    //ActionType: "Create"
    //IsActive: true
    //PermissionType: "Feature"
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
    }
}
