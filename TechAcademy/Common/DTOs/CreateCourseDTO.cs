using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Course title is required.")]
        [MaxLength(200, ErrorMessage = "Course title cannot exceed 50 characters.")]
        [MinLength(2, ErrorMessage = "Course title cannot contain less than 2 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Course description is required.")]
        [MaxLength(1000, ErrorMessage = "Course description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [MaxLength(50, ErrorMessage = "Instructor username cannot exceed 50 characters.")]
        public string InstructorUsername { get; set; }
    }
}
