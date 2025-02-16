using Common.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Persistence.Entities;

namespace CSDproject.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        [Required(ErrorMessage = "Course is required.")]
        public Course Course { get; set; }

        [Required(ErrorMessage = "Course modules are required.")]
        [MinLength(1, ErrorMessage = "At least one course module is required.")]
        public List<CourseModuleDto> CourseModules { get; set; } = new List<CourseModuleDto>();

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }
    }
}

