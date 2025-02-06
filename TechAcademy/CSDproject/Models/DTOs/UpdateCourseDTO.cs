namespace CSDproject.Models.DTOs
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorId { get; set; }
    }
}