
using Microsoft.AspNetCore.Mvc;
using System;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services.Scoped;
using ISEPay.Common.Enums;
using Microsoft.AspNetCore.Authorization;

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

       
        // POST api/users/register
        
        [HttpPost("register")]
        [Authorize(Policy = "Public")]

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
        [Authorize(Policy = "Admin")]

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


        [HttpPost("register/agent")]
        [Authorize(Policy = "Admin")]

        public IActionResult RegisterAgent([FromBody] UserDTO user)
        {
            try
            {
                // Call the CreateUser service method
                _userService.CreateAgentUser(user);
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
        [Authorize(Policy = "Admin")]

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
        [Authorize(Policy = "Admin")]

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
        [Authorize(Policy = "Admin")]

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
        [Authorize(Policy = "Admin")]
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
        [Authorize(Policy = "Admin")]
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
        [Authorize(Policy = "Admin")]

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
