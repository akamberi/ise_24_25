using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CourseModuleDto
    {
        [Required(ErrorMessage = "Module ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Module ID must be a positive number.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Module name is required.")]
        [MaxLength(100, ErrorMessage = "Module name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a positive number.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters.")]
        public string Content { get; set; }  // Content to be displayed

        [Required(ErrorMessage = "Course name is required.")]
        [MaxLength(100, ErrorMessage = "Course name cannot exceed 100 characters.")]
        public string CourseName { get; set; } // Add this property

        [Required(ErrorMessage = "Lesson files are required.")]
        [MinLength(1, ErrorMessage = "At least one lesson file is required.")]
        public IEnumerable<LessonFileDTO> LessonFiles { get; set; } // Add this property


    }

}
