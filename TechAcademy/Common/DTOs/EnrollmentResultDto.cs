using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class EnrollmentResultDto
    {
        [Required(ErrorMessage = "Enrollment result success flag is required.")]
        public bool IsSuccessful { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }
    }
}

