using BLL.Interfaces;
using BLL.Services;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSDproject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseModuleController : ControllerBase
    {
        private readonly ICourseModuleService _courseModuleService;

        public CourseModuleController(ICourseModuleService courseModuleService)
        {
            _courseModuleService = courseModuleService;
        }

        // Get all course modules
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var modules = await _courseModuleService.GetAllAsync();
            return Ok(modules);
        }

        // Get a course module by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            if (module == null)
                return NotFound();

            return Ok(module);
        }

        // Create a new course module
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseModuleDto dto)
        {
            var createdModule = await _courseModuleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdModule.Id }, createdModule);
        }

        // Update an existing course module
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseModuleDto dto)
        {
            var updatedModule = await _courseModuleService.UpdateAsync(id, dto);
            if (updatedModule == null)
                return NotFound();

            return Ok(updatedModule);
        }

        // Delete a course module
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseModuleService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
