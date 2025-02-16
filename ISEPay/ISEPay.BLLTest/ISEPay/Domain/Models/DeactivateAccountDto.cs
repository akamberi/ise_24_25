using System;
using System.ComponentModel.DataAnnotations;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class DeactivateAccountDto
    {
        [Required(ErrorMessage = "UserId is required")]
        [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$",
            ErrorMessage = "UserId must be a valid GUID")]
        public Guid UserId { get; set; }

        
        [Required(ErrorMessage = "Account number is required")]
        [StringLength(12, ErrorMessage = "Account number must be exactly 12 characters long", MinimumLength = 12)]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Account number must be alphanumeric and in uppercase")]
        public string AccountNumber { get; set; }
    }
}
