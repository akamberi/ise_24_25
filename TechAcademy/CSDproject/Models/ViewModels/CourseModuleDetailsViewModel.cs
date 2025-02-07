using Common.DTOs;

namespace CSDproject.Models.ViewModels
{
    public class CourseModuleDetailsViewModel
    {
        public CourseModuleDto CourseModule { get; set; }
        public List<LessonFileDTO> LessonFiles { get; set; }
    }

}
