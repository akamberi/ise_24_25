using Common.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSDproject.Models.ViewModels
{
    public class CourseModuleDetailsViewModel
    {
        [Required(ErrorMessage = "Course module is required.")]
        public CourseModuleDto CourseModule { get; set; }

        [Required(ErrorMessage = "Lesson files are required.")]
        [MinLength(1, ErrorMessage = "At least one lesson file is required.")]
        public List<LessonFileDTO> LessonFiles { get; set; } = new List<LessonFileDTO>();
    }
}

