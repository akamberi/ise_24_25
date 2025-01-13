using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign key to IdentityUser (Student)
        public IdentityUser User { get; set; }  // Reference to IdentityUser
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
