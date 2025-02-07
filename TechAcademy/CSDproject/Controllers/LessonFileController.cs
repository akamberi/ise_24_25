using BLL.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CSDproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonFileController : ControllerBase
    {
        private readonly ILessonFileService _lessonFileService;

        public LessonFileController(ILessonFileService lessonFileService)
        {
            _lessonFileService = lessonFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _lessonFileService.GetAllAsync();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var file = await _lessonFileService.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            return Ok(file);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] CreateLessonFileDTO dto)
        {
            try
            {
                var lessonFile = await _lessonFileService.UploadAsync(dto);
                return Ok(lessonFile); // or return a specific response
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle errors appropriately
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _lessonFileService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "File not found or could not be deleted." });
            }

            return Ok(new { message = "File deleted successfully." });
        }
    

    }
}
