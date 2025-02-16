using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class PaymentResultDto
    {
        [Required(ErrorMessage = "Payment success flag is required.")]
        public bool IsSuccessful { get; set; }

        [Required(ErrorMessage = "Transaction ID is required.")]
        [MaxLength(100, ErrorMessage = "Transaction ID cannot exceed 100 characters.")]
        public string TransactionId { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        [MaxLength(1000, ErrorMessage = "Redirect URL cannot exceed 1000 characters.")]
        public string RedirectUrl { get; set; } // Optional, but valid URL if present
    }
}
