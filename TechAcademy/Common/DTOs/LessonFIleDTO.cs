using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class LessonFileDTO
    {
        public int Id { get; set; }
        public int CourseModuleId { get; set; }
        public string CourseModuleName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
    }
}
