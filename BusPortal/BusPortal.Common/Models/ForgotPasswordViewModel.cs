using System.ComponentModel.DataAnnotations;

namespace BusPortal.Common.Models;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
