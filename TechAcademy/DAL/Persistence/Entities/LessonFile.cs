using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DAL.Persistence.Entities
    {
    public class LessonFile
    {
        public int Id { get; set; }
        public int CourseModuleId { get; set; }  // Foreign key to CourseModule
        public CourseModule CourseModule { get; set; }
        public string FileName { get; set; }  // e.g., "lesson1.mp4"
        public string FilePath { get; set; }  // Path to the file storage location
        public string FileType { get; set; }  // e.g., "mp4", "pdf", "audio"
        public long FileSize { get; set; }  // Size of the file in bytes
    }

}
