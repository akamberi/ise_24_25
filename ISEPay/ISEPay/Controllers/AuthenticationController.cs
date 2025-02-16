using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ISEPay.Controllers
{
    [ApiController]
    [Route("")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //Users pass: 12345678
        //Users pass: Isepay2025.
        //Admin pass: admin123
        [HttpPost("authenticate")]
        [Authorize(Policy = "Public")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                // Validate the input model
                if (request == null)
                    return BadRequest("Request body cannot be null.");

                // Perform authentication
                var response = _authenticationService.Authenticate(request);

                // Return the response
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
