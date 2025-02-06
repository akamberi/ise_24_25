using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public string InstructorUsername { get; set; }
    }
}