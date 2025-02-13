using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PaymentResultDto
    {
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; } // New property for frontend redirection
    }
}
