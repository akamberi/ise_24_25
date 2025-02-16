using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Persistence.Entities;
using Common.DTOs;
using DAL.Data;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        
        Task<Course> CreateCourseAsync(CreateCourseDto courseDto);
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task UpdateCourseAsync(UpdateCourseDto courseDto);
        Task DeleteCourseAsync(int courseId);
    }
}