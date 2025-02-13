using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Entities
{
    public class CourseEnrollment
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign key to IdentityUser (Student)
        public IdentityUser User { get; set; }  // Reference to IdentityUser (Student)
        public int CourseId { get; set; }  // Foreign key to Course
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}
