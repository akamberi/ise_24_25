using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class EnrollStudentDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Course ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a positive number.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [MaxLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        [RegularExpression(@"^(PayPal|CreditCard|BankTransfer)$", ErrorMessage = "Payment method must be PayPal, CreditCard, or BankTransfer.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Enrollment success status is required.")]
        public bool IsSuccessful { get; set; }
    }
}

