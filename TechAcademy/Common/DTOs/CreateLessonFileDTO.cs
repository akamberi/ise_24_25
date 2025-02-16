using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateLessonFileDTO
    {
        [Required(ErrorMessage = "Course Module ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course Module ID must be a positive number.")]
        public int CourseModuleId { get; set; }

        [Required(ErrorMessage = "File is required.")]
        
        public IFormFile File { get; set; }
    }


   
}