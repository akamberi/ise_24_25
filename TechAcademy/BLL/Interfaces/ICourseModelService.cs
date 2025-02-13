using Common.DTOs;
using DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourseModuleService
    {
        Task<IEnumerable<CourseModuleDto>> GetAllAsync();  // Get all course modules
        Task<CourseModuleDto?> GetByIdAsync(int id);  // Get course module by ID
        Task<CourseModuleDto> CreateAsync(CreateCourseModuleDto dto);  // Create new course module
        Task<CourseModuleDto?> UpdateAsync(int id, UpdateCourseModuleDto dto);  // Update existing course module
        Task<bool> DeleteAsync(int id);  // Delete a course module
        Task<IEnumerable<CourseModule>> GetModulesByCourseIdAsync(int courseId);
        

    }
}
