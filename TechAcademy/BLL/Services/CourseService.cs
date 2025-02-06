using BLL.Interfaces;
using DAL.Persistence.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Common.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourseAsync(CreateCourseDto courseDto)
        {
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                InstructorId = courseDto.InstructorUsername
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.CourseModules)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.CourseModules)
                .ToListAsync();
        }

        public async Task UpdateCourseAsync(UpdateCourseDto courseDto)
        {
            var course = await _context.Courses.FindAsync(courseDto.Id);
            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.InstructorId = courseDto.InstructorUsername;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}