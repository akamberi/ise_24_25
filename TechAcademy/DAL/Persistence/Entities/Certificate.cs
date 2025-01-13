using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign key to IdentityUser (Student)
        public IdentityUser User { get; set; }  // Reference to IdentityUser (Student)
        public int CourseId { get; set; }  // Foreign key to Course
        public Course Course { get; set; }  // Reference to Course
        public string CertificateUrl { get; set; }  // URL or file path for the certificate (if generated as a PDF)
        public DateTime IssuedDate { get; set; }
        public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; }  // Link to submissions
    }


}
