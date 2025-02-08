using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CourseEnrollmentController : ControllerBase
{
    private readonly ICourseEnrollmentService _enrollmentService;

    public CourseEnrollmentController(ICourseEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }


    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollStudent([FromBody] CourseEnrollmentRequest request)
    {
        var result = await _enrollmentService.EnrollStudentInCourseAsync(request.CourseId, request.StudentId);
        if (!result)
        {
            return BadRequest("Student is already enrolled in this course.");
        }
        return Ok("Enrollment successful.");
    }
}



public class CourseEnrollmentRequest
{
    public int CourseId { get; set; }
    public string StudentId { get; set; }
}
