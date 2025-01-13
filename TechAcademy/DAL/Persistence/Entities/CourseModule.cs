using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Persistence.Entities
{
    public class CourseModule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }  // Foreign key to Course
        public Course Course { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<LessonFile> LessonFiles { get; set; }
    }
}
