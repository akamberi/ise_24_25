using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CourseModuleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string Content { get; set; }  // Content to be displayed
        public string CourseName { get; set; } // Add this property
        public IEnumerable<LessonFileDTO> LessonFiles { get; set; } // Add this property


    }

}
