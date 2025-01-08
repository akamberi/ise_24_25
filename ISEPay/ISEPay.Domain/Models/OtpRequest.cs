using System.ComponentModel.DataAnnotations;

namespace ISEPay.Domain.Models
{
    public class OtpRequest
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "PhoneNumber must be numeric and between 10 to 15 digits.")]
        public string PhoneNumber { get; set; }

        // Validation logic: At least one field should be provided
        public bool IsValid()
        {
            // Check if at least one of the fields is provided
            return !(string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNumber));
        }
    }
}
