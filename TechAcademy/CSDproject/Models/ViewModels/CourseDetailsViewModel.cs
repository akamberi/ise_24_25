using Common.DTOs;
using DAL.Persistence.Entities;

namespace CSDproject.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; }
        public List<CourseModuleDto> CourseModules { get; set; }
    }

}
