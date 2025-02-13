using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using Common.DTOs;
using DAL.Persistence.Entities;
using DAL.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<CourseEnrollmentService> _logger;

        public CourseEnrollmentService(ICourseEnrollmentRepository courseEnrollmentRepository,
                                        IPaymentService paymentService,
                                        ILogger<CourseEnrollmentService> logger)
        {
            _courseEnrollmentRepository = courseEnrollmentRepository;
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<EnrollmentResultDto> EnrollStudentAsync(string userId, int courseId, string transactionId)
        {
            try
            {
                // Verify payment before enrollment
                bool isPaymentValid = await _paymentService.VerifyPaymentAsync(transactionId);
                if (!isPaymentValid)
                {
                    _logger.LogWarning($"Payment verification failed for Transaction ID: {transactionId}");
                    return new EnrollmentResultDto { IsSuccessful = false, Message = "Invalid payment." };
                }

                var enrollment = new CourseEnrollment
                {
                    UserId = userId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.UtcNow,
                    IsCompleted = false
                };

                var result = await _courseEnrollmentRepository.EnrollStudentAsync(enrollment);
                if (result == null)
                {
                    return new EnrollmentResultDto { IsSuccessful = false, Message = "Student is already enrolled." };
                }

                return new EnrollmentResultDto { IsSuccessful = true, Message = "Enrollment successful." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error enrolling student: {ex.Message}");
                return new EnrollmentResultDto { IsSuccessful = false, Message = "Enrollment failed." };
            }
        }

        public Task<IEnumerable<CourseEnrollment>> GetAllEnrollmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseEnrollment> GetEnrollmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseEnrollment>> GetEnrollmentsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsStudentEnrolledAsync(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveEnrollmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<EnrollmentResultDto> ICourseEnrollmentService.EnrollStudentAsync(string userId, int courseId, string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
