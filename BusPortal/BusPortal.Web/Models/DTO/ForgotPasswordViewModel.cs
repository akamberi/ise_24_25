using System.ComponentModel.DataAnnotations;

namespace BusPortal.Web.Models.DTO
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
