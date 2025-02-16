using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class CreateAssignmentSubmissionDto
    {
        [Required(ErrorMessage = "Assignment ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Assignment ID must be a positive number.")]
        public int AssignmentId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Submission date is required.")]
        public DateTime SubmittedDate { get; set; }

        [Required(ErrorMessage = "Submission status is required.")]
        public bool IsSubmitted { get; set; }

        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
        public double? Score { get; set; }
    }

}
