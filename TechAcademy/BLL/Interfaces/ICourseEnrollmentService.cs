﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;
using DAL.Persistence.Entities;

namespace BLL.Interfaces
{
    public interface ICourseEnrollmentService
    {
        Task<IEnumerable<CourseEnrollment>> GetAllEnrollmentsAsync();
        Task<CourseEnrollment> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByUserIdAsync(string userId);
        Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<bool> IsStudentEnrolledAsync(string userId, int courseId);
        Task<bool> RemoveEnrollmentAsync(int id);
        Task<EnrollmentResultDto> EnrollStudentAsync(string userId, int courseId, string transactionId);

    }
}
