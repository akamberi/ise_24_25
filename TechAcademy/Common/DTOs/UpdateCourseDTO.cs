using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
         [Required]
        public decimal Price { get; set; } 
        public string InstructorUsername { get; set; }
    }
}