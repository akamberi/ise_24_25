using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GoogleFormUrl { get; set; }  // Link to Google Form
        public int CourseId { get; set; }  // Foreign key to Course
        public Course Course { get; set; }  // Reference to Course
        public DateTime DueDate { get; set; }
    }

}
