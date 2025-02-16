using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreateCourseModuleDto
    {
        [Required(ErrorMessage = "Module name is required.")]
        [MaxLength(100, ErrorMessage = "Module name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;  // Name property

        [Required(ErrorMessage = "Course ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a positive number.")]
        public int CourseId { get; set; }  // Foreign key to Course


        [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters.")]
        public string Content { get; set; }  // Nullable content

    }
}
