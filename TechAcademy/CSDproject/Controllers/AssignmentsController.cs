using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // For SelectList
using BLL.Interfaces;
using Common.DTOs;
using System.Threading.Tasks;
using BLL.Services;
using Google;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CSDproject.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ICourseService _courseService; // Inject course service
        private readonly GoogleSheetsService _googleSheetsService;


        public AssignmentsController(IAssignmentService assignmentService, ICourseService courseService, GoogleSheetsService googleSheetsService)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
            _googleSheetsService = googleSheetsService;

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
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Results()
        {
            // Use the correct tab name here:
            string spreadsheetId = "1YhmxdBJ3apIPir4ETzTQenaa9bLgqCC8ar-BIVmOOQ0";
            string range = "'Form Responses 1'!A:E";  // Must match the actual sheet tab name

            try
            {
                var data = await _googleSheetsService.GetData(spreadsheetId, range);

                // Get the logged-in user's email
                string loggedInStudentEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(loggedInStudentEmail))
                {
                    ViewBag.ErrorMessage = "Unable to identify the logged-in student.";
                    return View(new List<dynamic>());
                }

                Console.WriteLine($"Logged-in Email: {loggedInStudentEmail}");

                var filteredResults = new List<dynamic>();

                // Skip the header row (row[0]) and iterate data rows
                foreach (var row in data.Skip(1))
                {
                    // Make sure the row has at least 3 columns: Timestamp, Email, Score
                    if (row.Count < 3) continue;

                    string studentEmail = row[1]?.ToString();  // Email is column B => index 1
                    string scoreString = row[2]?.ToString();  // Score is column C => index 2

                    if (!string.IsNullOrEmpty(studentEmail) &&
                        studentEmail.Trim().Equals(loggedInStudentEmail.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        // Try parsing out the numeric portion of "6/10"
                        int numericScore = 0;
                        if (!string.IsNullOrEmpty(scoreString) && scoreString.Contains("/"))
                        {
                            var parts = scoreString.Split('/');
                            if (parts.Length > 0)
                            {
                                int.TryParse(parts[0], out numericScore);
                            }
                        }
                        else
                        {
                            // If it's just "6", "7" or something else, try parse directly
                            int.TryParse(scoreString, out numericScore);
                        }

                        var result = new
                        {
                            Score = scoreString,              // Keep the original "6/10" for display
                            Status = numericScore >= 6 ? "Pass" : "Fail"
                        };

                        filteredResults.Add(result);
                    }
                }

                if (!filteredResults.Any())
                {
                    ViewBag.ErrorMessage = "No results found for your email.";
                }

                return View(filteredResults);
            }
            catch (GoogleApiException ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the data. " + ex.Message;
                return View(new List<dynamic>());
            }
        }


    }
}