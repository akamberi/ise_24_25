using BLL.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using BLL.Services;

namespace CSDproject.Controllers.Normal
{
    public class LessonFileController : Controller
    {
        private readonly ILessonFileService _lessonFileService;
        private readonly ICourseModuleService _courseModuleService;
        private readonly string _uploadPath = @"C:\Users\User\Desktop\Uploads";

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


        public async Task<IActionResult> Create()
        {
            // Await the GetAllAsync method to get the result
            var courseModules = await _courseModuleService.GetAllAsync();

            // Use the result in SelectList
            ViewBag.CourseModules = new SelectList(courseModules, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLessonFileDTO dto)
        {
            if (ModelState.IsValid)
            {
                var uploadedFile = await _lessonFileService.UploadAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseModules = new SelectList((System.Collections.IEnumerable)_courseModuleService.GetAllAsync(), "Id", "Name");
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

            if (file.FileType == "application/vnd.ms-powerpoint" ||
                file.FileType == "application/vnd.openxmlformats-officedocument.presentationml.presentation")
            {
                var pdfContent = await _lessonFileService.ConvertPptxToPdfAsync(id);
                if (pdfContent == null) return NotFound("Error converting PowerPoint to PDF");

                return File(pdfContent, "application/pdf"); // Serve the converted PDF
            }

            var fileContent = await _lessonFileService.GetFileContentAsync(id);
            return File(fileContent, file.FileType);
        }






    }
}

