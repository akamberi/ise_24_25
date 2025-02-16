using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class UpdateCourseModuleDto
    {
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string? Name { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string? Content { get; set; }  // Nullable content for updating
    }
}
