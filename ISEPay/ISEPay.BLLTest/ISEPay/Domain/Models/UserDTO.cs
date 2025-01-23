using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ISEPay.Common.Enums;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Full name is required")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Full name cannot exceed 50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Card ID is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Card ID must be exactly 10 digits")]
        public string CardId { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'")]
        public string Gender { get; set; }
    }
}
