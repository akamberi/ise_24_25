using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Common.DTOs;
using System.Threading.Tasks;

namespace CSDproject.Controllers
{
    // Require authentication for all actions in this controller.
    [Authorize]
    public class AssignmentSubmissionsController : Controller
    {
        private readonly IAssignmentSubmissionService _submissionService;

        public AssignmentSubmissionsController(IAssignmentSubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        // GET: /AssignmentSubmissions/Create?assignmentId=1
        // Only students can create a submission.
        [Authorize(Roles = "Student,Lecturer")]
        public IActionResult Create(int assignmentId)
        {
            // Pre-fill the DTO with the assignmentId and current date/time.
            var dto = new CreateAssignmentSubmissionDto
            {
                AssignmentId = assignmentId,
                SubmittedDate = System.DateTime.UtcNow,
                IsSubmitted = false
            };

            return View(dto);
        }

        // POST: /AssignmentSubmissions/Create
        // Only students can submit their work.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student, Lecturer")]
        public async Task<IActionResult> Create(CreateAssignmentSubmissionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var submission = await _submissionService.CreateAssignmentSubmissionAsync(dto);
            return RedirectToAction(nameof(Details), new { id = submission.Id });
        }

        // GET: /AssignmentSubmissions/Index
        // students teachers can view the complete list of submissions.
        [Authorize(Roles = "Lecturer,Student")]
        public async Task<IActionResult> Index()
        {
            var submissions = await _submissionService.GetAllAssignmentSubmissionsAsync();
            return View(submissions);
        }

        // GET: /AssignmentSubmissions/Details/5
        // Both teachers and students can view details of a submission.
        [Authorize(Roles = "Lecturer,Student")]
        public async Task<IActionResult> Details(int id)
        {
            var submission = await _submissionService.GetAssignmentSubmissionByIdAsync(id);
            if (submission == null)
                return NotFound();

            return View(submission);
        }
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Edit(int id)
        {
            var submission = await _submissionService.GetAssignmentSubmissionByIdAsync(id);
            if (submission == null)
                return NotFound();

            var dto = new CreateAssignmentSubmissionDto
            {
                AssignmentId = submission.AssignmentId,
                UserId = submission.UserId,
                SubmittedDate = submission.SubmittedDate,
                IsSubmitted = submission.IsSubmitted,
                Score = submission.Score
            };

            ViewBag.SubmissionId = submission.Id; // This is critical for the form route.
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAssignmentSubmissionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);


            await _submissionService.UpdateAssignmentSubmissionAsync(id, dto);

            return RedirectToAction(nameof(Details), new { id = id });
            

        }
        


    }
}
