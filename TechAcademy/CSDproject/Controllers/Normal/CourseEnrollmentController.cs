using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CSDproject.Controllers.Normal
{
    [Authorize]
    public class CourseEnrollmentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseEnrollmentController(IPaymentService paymentService, ICourseEnrollmentService courseEnrollmentService)
        {
            _paymentService = paymentService;
            _courseEnrollmentService = courseEnrollmentService;
        }

        public IActionResult PaymentCreate(int courseId, decimal amount)
        {
            ViewBag.CourseId = courseId;
            ViewBag.Amount = amount;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(int courseId, decimal amount)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var paymentResult = await _paymentService.ProcessPaymentAsync(userId, courseId, amount);

            if (!paymentResult.IsSuccessful)
            {
                TempData["Error"] = "Payment failed.";
                return RedirectToAction("PaymentFailure", new { courseId });
            }

            // Enroll the student after payment is successful
            var enrollmentResult = await _courseEnrollmentService.EnrollStudentAsync(userId, courseId, paymentResult.TransactionId);

            if (!enrollmentResult.IsSuccessful)
            {
                TempData["Error"] = "Enrollment failed.";
                return RedirectToAction("PaymentFailure", new { courseId });
            }

            // Redirect to the course modules after enrollment
            return RedirectToAction("Index", "CourseModule", new { id = courseId });
        }


        public async Task<IActionResult> EnrollStudent(int courseId, string transactionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var enrollmentResult = await _courseEnrollmentService.EnrollStudentAsync(userId, courseId, transactionId);

            if (!enrollmentResult.IsSuccessful)
            {
                TempData["Error"] = "Enrollment failed.";
                return RedirectToAction("PaymentCreate", new { courseId });
            }

            return RedirectToAction("Index", "CourseModule", new { courseId });
        }

        public IActionResult PaymentSuccess(int courseId, string transactionId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        public IActionResult PaymentFailure(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

    }
}
