

using ISEPay.Common.Enums;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class UserDTO
    {

        public string FullName { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

       // public string Otp { get; set; }

       


    }
}
