using BLL.Interfaces;
using Common.DTOs;
using DAL.Data;
using DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services
{
    public class CourseModuleService : ICourseModuleService
    {
        private readonly ApplicationDbContext _context;

        public CourseModuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all course modules
        public async Task<IEnumerable<CourseModuleDto>> GetAllAsync()
        {
            return await _context.CourseModules
                .Join(_context.Courses,
                      cm => cm.CourseId,
                      course => course.Id,
                      (cm, course) => new CourseModuleDto
                      {
                          Id = cm.Id,
                          Name = cm.Title,
                          CourseId = cm.CourseId,
                          Content = cm.Content,
                          CourseName = course.Title // Fetch Course Name
                      })
                .ToListAsync();
        }

        // Get a course module by its ID
        public async Task<CourseModuleDto?> GetByIdAsync(int id)
        {
            var courseModule = await _context.CourseModules
                .Where(cm => cm.Id == id)
                .Join(_context.Courses,
                      cm => cm.CourseId,
                      course => course.Id,
                      (cm, course) => new CourseModuleDto
                      {
                          Id = cm.Id,
                          Name = cm.Title,
                          CourseId = cm.CourseId,
                          Content = cm.Content,
                          CourseName = course.Title // Fetch Course Name
                      })
                .FirstOrDefaultAsync();

            return courseModule;
        }



        // Create a new course module
        public async Task<CourseModuleDto> CreateAsync(CreateCourseModuleDto dto)
        {
            var courseModule = new CourseModule
            {
                Title = dto.Name,
                CourseId = dto.CourseId,
                Content = dto.Content   // Default to empty if Content is null
            };

            _context.CourseModules.Add(courseModule);
            await _context.SaveChangesAsync();

            return new CourseModuleDto
            {
                Id = courseModule.Id,
                Name = courseModule.Title,
                CourseId = courseModule.CourseId,
                Content = courseModule.Content
            };
        }

        // Update an existing course module
        public async Task<CourseModuleDto?> UpdateAsync(int id, UpdateCourseModuleDto dto)
        {
            var existingModule = await _context.CourseModules.FindAsync(id);
            if (existingModule == null)
                return null;

            // Preserve existing values if not provided
            if (!string.IsNullOrEmpty(dto.Name))
                existingModule.Title = dto.Name;
            if (!string.IsNullOrEmpty(dto.Content))
                existingModule.Content = dto.Content;

            await _context.SaveChangesAsync();

            return new CourseModuleDto
            {
                Id = existingModule.Id,
                Name = existingModule.Title,
                CourseId = existingModule.CourseId,
                Content = existingModule.Content
            };
        }


        // Delete a course module
        public async Task<bool> DeleteAsync(int id)
        {
            var courseModule = await _context.CourseModules.FindAsync(id);
            if (courseModule == null)
                return false;

            _context.CourseModules.Remove(courseModule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CourseModule>> GetModulesByCourseIdAsync(int courseId)
        {
            return await _context.CourseModules
                                 .Where(cm => cm.CourseId == courseId)
                                 .ToListAsync();
        }

    }

}
