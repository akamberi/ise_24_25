using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class AuthenticationResponse

    {

        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Optionally, you can add a constructor to initialize the properties
        public AuthenticationResponse(Guid userID, string name, string email, string phoneNumber)
        {
            UserID = userID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
