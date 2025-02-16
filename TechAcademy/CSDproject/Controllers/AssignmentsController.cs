using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // For SelectList
using BLL.Interfaces;
using Common.DTOs;
using System.Threading.Tasks;

namespace CSDproject.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ICourseService _courseService; // Inject course service

        public AssignmentsController(IAssignmentService assignmentService, ICourseService courseService)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
        }

        // GET: /Assignments/Index
        // List all assignments - accessible to all (professors & students)
        public async Task<IActionResult> Index()
        {
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
            return View(assignments);
        }

        // GET: /Assignments/Details/5
        // Show details of a specific assignment - accessible to all
        public async Task<IActionResult> Details(int id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
                return NotFound();
            return View(assignment);
        }

        // GET: /Assignments/Create
        // Display a form for creating a new assignment - professors only
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Create()
        {
            // Retrieve courses from the course service.
            var courses = await _courseService.GetAllCoursesAsync();
            // Create a SelectList with the course Id as value and Title as display text.
            ViewBag.Courses = new SelectList(courses, "Id", "Title");

            return View();
        }

        // POST: /Assignments/Create
        // Process the creation of a new assignment - professors only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Create(CreateAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var assignment = await _assignmentService.CreateAssignmentAsync(dto);
            return RedirectToAction(nameof(Details), new { id = assignment.Id });
        }

        // GET: /Assignments/Edit/5
        // Display a form for editing an existing assignment - professors only
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Edit(int id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
                return NotFound();

            var dto = new CreateAssignmentDto
            {
                Title = assignment.Title,
                Description = assignment.Description,
                GoogleFormUrl = assignment.GoogleFormUrl,
                CourseId = assignment.CourseId,
                DueDate = assignment.DueDate
            };

            ViewBag.AssignmentId = assignment.Id;
            return View(dto);
        }

        // POST: /Assignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Edit(int id, CreateAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _assignmentService.UpdateAssignmentAsync(id, dto);
            return RedirectToAction(nameof(Details), new { id = id });
        }

        // GET: /Assignments/Delete/5
        // Display a confirmation page for deletion - professors only
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
                return NotFound();
            return View(assignment);
        }

        // POST: /Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _assignmentService.DeleteAssignmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
