using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreateCourseModuleDto
    {
        public string Name { get; set; } = string.Empty;  // Name property
        public int CourseId { get; set; }  // Foreign key to Course
        public string Content { get; set; }  // Nullable content

    }
}
