using System.Security.Claims;
using BLL.Interfaces;
using Common.DTOs;
using DAL.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSDproject.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollmentController : ControllerBase
    {
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseEnrollmentController(ICourseEnrollmentService courseEnrollmentService)
        {
            _courseEnrollmentService = courseEnrollmentService;
        }

        // Endpoint to get all enrollments
        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _courseEnrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        // Endpoint to get enrollment by userId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEnrollmentsByUserId(string userId)
        {
            var enrollments = await _courseEnrollmentService.GetEnrollmentsByUserIdAsync(userId);
            return Ok(enrollments);
        }

        // Endpoint to enroll a student after successful payment
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent(int courseId, string transactionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _courseEnrollmentService.EnrollStudentAsync(userId, courseId, transactionId);

            if (!result.IsSuccessful)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = "Enrollment successful!" });
        }

        // Endpoint to get current logged-in user's enrollments
        [HttpGet("my-enrollments")]
        public async Task<IActionResult> GetMyEnrollments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var enrollments = await _courseEnrollmentService.GetEnrollmentsByUserIdAsync(userId);
            return Ok(enrollments);
        }
    }
}
