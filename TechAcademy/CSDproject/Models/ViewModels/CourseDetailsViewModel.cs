using Common.DTOs;
using System.Collections.Generic;
using DAL.Persistence.Entities;

namespace CSDproject.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; }
        public List<CourseModuleDto> CourseModules { get; set; }
        public decimal Price { get; set; }
    }

}
