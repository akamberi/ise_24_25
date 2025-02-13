using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Persistence.Entities;

namespace DAL.Persistence.Repositories
{
    public interface ICourseEnrollmentRepository
    {
        Task<IEnumerable<CourseEnrollment>> GetAllEnrollmentsAsync();
        Task<CourseEnrollment> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByUserIdAsync(string userId);
        Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<CourseEnrollment> EnrollStudentAsync(CourseEnrollment enrollment);
        Task<bool> IsStudentEnrolledAsync(string userId, int courseId);
        Task<bool> RemoveEnrollmentAsync(int id);

    }
}
