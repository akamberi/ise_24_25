using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;



namespace DAL.Persistence.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorId { get; set; }  // Foreign key to IdentityUser (Lecturer)
        public IdentityUser Instructor { get; set; }  // Reference to IdentityUser (Lecturer)
        public ICollection<CourseModule> CourseModules { get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }  // Link to assignments
        public ICollection<Payment> Payments { get; set; }
    }

}