using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;


namespace Common.DTOs
{
    public class CreateAssignmentDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Google Form URL is required.")]
        [Url(ErrorMessage = "Please provide a valid URL.")]
        public string GoogleFormUrl { get; set; }

        [Required(ErrorMessage = "Course ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course ID must be a positive number.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
       
        public DateTime DueDate { get; set; }
    }

    
}