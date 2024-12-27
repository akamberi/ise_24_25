using System.ComponentModel.DataAnnotations;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class AuthenticationRequest
    {
        // Ensuring that email is required and in a valid email format
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        // Ensuring that password is required and has a minimum length of 6 characters
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
