using System.ComponentModel.DataAnnotations;

namespace CSDproject.Models.DTOs
{
    public class UpdateCourseDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Course Id must be a positive number.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Instructor ID is required.")]
        public string InstructorId { get; set; } // Ensure it's a valid non-empty string
    }
}
