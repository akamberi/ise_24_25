using System;


namespace Common.DTOs
{
    public class CreateAssignmentSubmissionDto
    {
        public int AssignmentId { get; set; }
        public string UserId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public bool IsSubmitted { get; set; }
        public double? Score { get; set; }
        
    }

}
