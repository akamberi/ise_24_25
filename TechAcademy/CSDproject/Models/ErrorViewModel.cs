using System.ComponentModel.DataAnnotations;

namespace CSDproject.Models
{
    public class ErrorViewModel
    {
        [MaxLength(50, ErrorMessage = "Request ID cannot exceed 50 characters.")]
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

