using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
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

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [MaxLength(100, ErrorMessage = "Instructor Username cannot exceed 100 characters.")]
        public string InstructorUsername { get; set; }
    }
}
