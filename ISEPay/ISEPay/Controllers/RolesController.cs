using ISEPay.BLL.Services.Scoped;
using ISEPay.BLL.ISEPay.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ISEPay.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        // Injecting the RoleService through the constructor
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

      
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult CreateRole([FromBody] RoleDto roleDto)
        {
            if (roleDto == null)
            {
                return BadRequest("Role data is required.");
            }
            try
            {
                _roleService.CreateRole(roleDto);
                return Ok("Role created successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);  // For cases like "Role already exists"
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
