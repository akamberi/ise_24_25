using System;

namespace ISEPay.Domain.Models
{
    public class UserResponse
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Optionally, you can add a constructor to initialize the properties
        public UserResponse(Guid userID, string name, string email, string phoneNumber)
        {
            UserID = userID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
