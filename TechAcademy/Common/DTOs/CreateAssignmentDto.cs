using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;


namespace Common.DTOs
{
    public class CreateAssignmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string GoogleFormUrl { get; set; }  // URL for the Google Form submission
        public int CourseId { get; set; }          // The course this assignment belongs to
        public DateTime DueDate { get; set; }
    }
}
