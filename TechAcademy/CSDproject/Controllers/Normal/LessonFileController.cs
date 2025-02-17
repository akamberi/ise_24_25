using BLL.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using BLL.Services;
using Aspose.Words;

namespace CSDproject.Controllers.Normal
{
    public class LessonFileController : Controller
    {
        private readonly ILessonFileService _lessonFileService;
        private readonly ICourseModuleService _courseModuleService;
        private readonly string _uploadPath = @"C:\Users\User\OneDrive\Desktop\Uploads\";

        public LessonFileController(ILessonFileService lessonFileService, ICourseModuleService courseModuleService)
        {
            _lessonFileService = lessonFileService;
            _courseModuleService = courseModuleService;
        }
        public async Task<IActionResult> Index()
        {
            var files = await _lessonFileService.GetAllAsync();
            return View(files);
        }


        public async Task<IActionResult> Create(int? CourseModuleId)
        {
            var courseModules = await _courseModuleService.GetAllAsync();
            ViewBag.CourseModules = new SelectList(courseModules, "Id", "Name");
            if (CourseModuleId.HasValue)
            {
                ViewBag.SelectedModuleId = CourseModuleId.Value;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLessonFileDTO dto)
        {
            if (ModelState.IsValid)
            {
                var uploadedFile = await _lessonFileService.UploadAsync(dto);

                // Redirect to Module Details if a CourseModuleId is present
                if (dto.CourseModuleId > 0)
                {
                    return RedirectToAction("Details", "CourseModule", new { id = dto.CourseModuleId });
                }

                // Otherwise, redirect to the default list
                return RedirectToAction(nameof(Index));
            }

            var courseModules = await _courseModuleService.GetAllAsync();
            ViewBag.CourseModules = new SelectList(courseModules, "Id", "Name");
            return View(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _lessonFileService.DeleteAsync(id);
            if (!isDeleted)
            {
                return Json(new { success = false, message = "File not found or could not be deleted." });
            }

            return Json(new { success = true, message = "File deleted successfully." });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _lessonFileService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var file = await _lessonFileService.GetByIdAsync(id);
            if (file == null) return NotFound();

            return View(file);
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = await _lessonFileService.GetByIdAsync(id);
            if (file == null) return NotFound();

            var fileContent = await _lessonFileService.GetFileContentAsync(id);
            return File(fileContent, file.FileType, file.FileName);
        }

        [HttpGet("view/{id}")]



        public async Task<IActionResult> ViewFile(int id)
        {
            var file = await _lessonFileService.GetByIdAsync(id);
            if (file == null) return NotFound();

            var filePath = file.FilePath;

            if (file.FileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") // DOCX
            {
                var pdfPath = Path.ChangeExtension(filePath, ".pdf");

                if (!System.IO.File.Exists(pdfPath)) // Convert only if not already converted
                {
                    var doc = new Document(filePath);
                    doc.Save(pdfPath, Aspose.Words.SaveFormat.Pdf);
                }

                var pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                return File(pdfBytes, "application/pdf");
            }

            var fileContent = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileContent, file.FileType);
        }


        [HttpGet("LessonFile/ListByModule/{moduleId}")]
        public async Task<IActionResult> ListByModule(int moduleId)
        {
            // Get all files
            var allFiles = await _lessonFileService.GetAllAsync();

            // Filter files by the selected module
            var moduleFiles = allFiles.Where(f => f.CourseModuleId == moduleId).ToList();

            // Retrieve the module details for display
            var module = await _courseModuleService.GetByIdAsync(moduleId);
            ViewBag.ModuleName = module?.Name;
            ViewBag.ModuleId = moduleId; // Set the module ID for use in the view

            return View(moduleFiles);
        }



    }
}
