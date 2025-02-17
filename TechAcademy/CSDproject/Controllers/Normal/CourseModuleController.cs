using BLL.Interfaces;
using Common.DTOs;
using CSDproject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
            if (courseModule == null)
                return NotFound();

            // Fetch lesson files related to this course module
            var lessonFiles = await _lessonFileService.GetAllAsync();
            var lessonFilesForModule = lessonFiles.Where(f => f.CourseModuleId == id).ToList();

            var viewModel = new CourseModuleDetailsViewModel
            {
                CourseModule = courseModule,
                LessonFiles = lessonFilesForModule
            };

            return View(viewModel);
        }

        // New action: Lists modules for a specific course.
        public async Task<IActionResult> ListByCourse(int courseId)
        {
            var modules = (await _courseModuleService.GetAllAsync())
                          .Where(m => m.CourseId == courseId)
                          .ToList();
            ViewBag.CourseId = courseId;
            return View(modules);
        }

        // Modified GET Create action accepts an optional courseId.
        public async Task<IActionResult> Create(int? courseId)
        {
            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = courses;
            if (courseId.HasValue)
            {
                ViewBag.SelectedCourseId = courseId.Value;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseModuleDto dto)
        {
            if (ModelState.IsValid)
            {
                // This POST may not be used when creating via AJAX.
                await _courseModuleService.CreateAsync(dto);
                return RedirectToAction("Details", "Courses", new { id = dto.CourseId });
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            if (module == null) return NotFound();

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

        public async Task<IActionResult> Delete(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            if (module == null) return NotFound();
            return View(module);
        }

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
