using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }  // Added Price field
        public string InstructorUsername { get; set; }

    }
}