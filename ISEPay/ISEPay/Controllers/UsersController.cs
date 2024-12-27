
using Microsoft.AspNetCore.Mvc;
using System;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services.Scoped;
using ISEPay.Common.Enums;

namespace ISEPay.Controllers
{
    [Route("users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor with dependency injection
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //Users pass: 12345678
        //Admin pass: admin123
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (authenticationRequest == null)
            {
                return BadRequest(new { Message = "Invalid authentication request." });
            }

            try
            {
                // Call the Authenticate method from AuthenticationService
                var response = _userService.Authenticate(authenticationRequest);

                // Return a 200 OK response with the AuthenticationResponse
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors, e.g., missing email/password
                return BadRequest(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle invalid credentials
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, new { Message = "An error occurred during authentication.", Error = ex.Message });
            }
        }

        // POST api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO user)
        {
            try
            {
                // Call the CreateUser service method
                _userService.CreateUser(user);
                // Return a success response
                return Ok(new { Message = "User created successfully." });
            }
            catch (InvalidOperationException ex)
            {
                // Return a BadRequest if the user already exists
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a general error if something unexpected happens
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }


        [HttpPost("register/admin")]
        public IActionResult RegisterAdmin([FromBody] UserDTO user)
        {
            try
            {
                // Call the CreateUser service method
                _userService.CreateAdminUser(user);
                // Return a success response
                return Ok(new { Message = "User created successfully." });
            }
            catch (InvalidOperationException ex)
            {
                // Return a BadRequest if the user already exists
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a general error if something unexpected happens
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }


        [HttpPost("approve/{userId}")]
        public IActionResult ApproveUser(Guid userId)
        {
            try
            {
               
                _userService.ApproveUser(userId);
                return Ok(new { Message = "User approved successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }

        [HttpPost("reject/{userId}")]
        public IActionResult RejectUser(Guid userId)
        {
            try
            {

                _userService.RejectUser(userId);
                return Ok(new { Message = "User rejected successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }


        [HttpDelete("delete/{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            try
            {

                _userService.DeleteUser(userId);
                return Ok(new { Message = "User deleted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                // Call the GetAllUsers method
                var allUsers = _userService.GetAllUsers();  // Add parentheses to call the method
                return Ok(allUsers);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }

        [HttpGet("status")]
        public IActionResult GetUsersByStatus([FromQuery] UserStatus status)
        {
            try
            {
                // Call the GetUsersByStatus method in the service, passing the status
                var users = _userService.GetUsersByStatus(status);

                // Return the list of users as a successful response
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                // Return a 404 status code if no users are found for the specified status
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Return a 400 status code for bad request (if any invalid operation occurs)
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 status code for internal server errors
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(Guid userId)
        {
            try
            {
                // Call the GetUser method from the user service
                var user = _userService.GetUser(userId);

                // Return the user data with a 200 OK response
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                // Return a 404 Not Found if the user doesn't exist
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error if something goes wrong
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Error = ex.Message });
            }
        }



    }
}
