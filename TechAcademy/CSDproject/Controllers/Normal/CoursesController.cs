using BLL.Interfaces;
using DAL.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Common.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CSDproject.Controllers.Normal
{
    [Authorize(Roles = "Lecturer")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<IdentityUser> _userManager;


        public CoursesController(ICourseService courseService, UserManager<IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var createCourseDto = new CreateCourseDto
            {
                InstructorUsername = user.UserName 
            };

            return View(createCourseDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(courseDto);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            courseDto.InstructorUsername = user.Id;
            var createdCourse = await _courseService.CreateCourseAsync(courseDto);
            return RedirectToAction(nameof(Details), new { id = createdCourse.Id });
        }


        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(course.InstructorId);
            var username = user?.UserName ?? "Unknown";

            var updateCourseDto = new UpdateCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                InstructorUsername = username
            };

            return View(updateCourseDto);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCourseDto courseDto)
        {
            if (id != courseDto.Id)
            {
                return BadRequest("Course ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return View(courseDto);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            courseDto.InstructorUsername = user.Id;
            await _courseService.UpdateCourseAsync(courseDto);
            return RedirectToAction(nameof(Index));
        }


        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
