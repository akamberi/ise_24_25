using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreateLessonFileDTO
    {
            public int CourseModuleId { get; set; }
            public IFormFile File { get; set; }  // File upload
        
    }
}
