using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Entities
{
    public class AssignmentSubmission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }  // Foreign key to Assignment
        public Assignment Assignment { get; set; }  // Reference to Assignment
        public string UserId { get; set; }  // Foreign key to IdentityUser (Student)
        public IdentityUser User { get; set; }  // Reference to IdentityUser (Student)
        public DateTime SubmittedDate { get; set; }
        public bool IsSubmitted { get; set; }  // Track submission status
        public double? Score { get; set; }  // Score of the submission, can be null until graded
        public bool? PassFailStatus { get; set; }  // Pass (true) or Fail (false)
    }

}
