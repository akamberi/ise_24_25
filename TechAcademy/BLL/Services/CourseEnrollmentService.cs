using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EnrollStudentInCourseAsync(int courseId, string studentId)
        {
            var existingEnrollment = await _context.CourseEnrollments
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.UserId == studentId);

            if (existingEnrollment != null)
            {
                return false; // Already enrolled
            }

            var enrollment = new CourseEnrollment
            {
                CourseId = courseId,
                UserId = studentId
            };

            await _context.CourseEnrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
