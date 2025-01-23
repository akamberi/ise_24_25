using System;

namespace ISEPay.Domain.Models
{
    public class UserResponse
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CardId { get; set; }
        public string Picture { get; set; }  // Base64 encoded image

        // Optionally, you can add a constructor to initialize the properties
        public UserResponse(Guid userID, string name, string email, string phoneNumber, string cardId, string picture = null)
        {
            UserID = userID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            CardId = cardId;
            Picture = picture;
        }

        public UserResponse(Guid userID, string name, string email, string phoneNumber)
        {
            UserID = userID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

    }
}
