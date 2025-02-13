using BLL.Interfaces;
using Common.DTOs;
using CSDproject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CSDproject.Controllers.Normal
{
    public class CourseModuleController : Controller
    {
        private readonly ICourseModuleService _courseModuleService;
        private readonly ICourseService _courseService;
        private readonly ILessonFileService _lessonFileService;


        public CourseModuleController(ICourseModuleService courseModuleService, ICourseService courseService, ILessonFileService lessonFileService)
        {
            _courseModuleService = courseModuleService;
            _courseService = courseService;
            _lessonFileService = lessonFileService;
        }

        public async Task<IActionResult> Index()
        {
            var modules = await _courseModuleService.GetAllAsync();
            return View(modules);
        }


        public async Task<IActionResult> Details(int id)
        {
            var courseModule = await _courseModuleService.GetByIdAsync(id);
            if (courseModule == null) return NotFound();

            // Fetch lesson files related to this course module
            var lessonFiles = await _lessonFileService.GetAllAsync();

            // Filter lesson files by CourseModuleId (You can optimize this logic based on your service logic)
            var lessonFilesForModule = lessonFiles.Where(f => f.CourseModuleId == id).ToList();

            var viewModel = new CourseModuleDetailsViewModel
            {
                CourseModule = courseModule,
                LessonFiles = lessonFilesForModule
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            // Fetch list of courses and pass to the view
            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses; // Assuming GetAllCoursesAsync() returns a list of course objects

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseModuleDto dto)
        {
            if (ModelState.IsValid)
            {
                // ✅ Call API internally (assuming _courseModuleService calls the API)
                await _courseModuleService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));  // ✅ Redirect to show updated list
            }
            return View(dto);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            if (module == null) return NotFound();

            // Map CourseModuleDto to UpdateCourseModuleDto
            var updateDto = new UpdateCourseModuleDto
            {
                Name = module.Name,
                Content = module.Content
            };

            return View(updateDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCourseModuleDto dto)
        {
            if (ModelState.IsValid)
            {
                var updatedModule = await _courseModuleService.UpdateAsync(id, dto);
                if (updatedModule == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: CourseModule/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            if (module == null) return NotFound(); // If module is not found, return 404

            return View(module); // Return the confirmation view with the module data
        }

        // POST: CourseModule/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _courseModuleService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}