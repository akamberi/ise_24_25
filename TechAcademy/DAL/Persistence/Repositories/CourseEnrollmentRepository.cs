using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Persistence.Repositories
{
    public class CourseEnrollmentRepository : ICourseEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseEnrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.CourseEnrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<CourseEnrollment> GetEnrollmentByIdAsync(int id)
        {
            return await _context.CourseEnrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByUserIdAsync(string userId)
        {
            return await _context.CourseEnrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _context.CourseEnrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<CourseEnrollment> EnrollStudentAsync(CourseEnrollment enrollment)
        {
            var existingEnrollment = await _context.CourseEnrollments
                .FirstOrDefaultAsync(e => e.UserId == enrollment.UserId && e.CourseId == enrollment.CourseId);

            if (existingEnrollment == null)
            {
                await _context.CourseEnrollments.AddAsync(enrollment);
                await _context.SaveChangesAsync();
                return enrollment;
            }

            return null; // Student is already enrolled
        }

        public async Task<bool> IsStudentEnrolledAsync(string userId, int courseId)
        {
            return await _context.CourseEnrollments
                .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task<bool> RemoveEnrollmentAsync(int id)
        {
            var enrollment = await _context.CourseEnrollments.FindAsync(id);
            if (enrollment == null)
                return false;

            _context.CourseEnrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
