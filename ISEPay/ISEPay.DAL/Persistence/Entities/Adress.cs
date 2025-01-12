

namespace ISEPay.DAL.Persistence.Entities
{
    public class Address : BaseEntity<Guid>
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int? Zipcode { get; set; }
        
        
        
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
    }
}