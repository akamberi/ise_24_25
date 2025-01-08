/*using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services.Scoped;
using ISEPay.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISEPay.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService; 

       
        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        [Authorize(Policy = "Authenticated")]
        public IActionResult AddAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Call the AddAddress method from your service
                addressService.AddAddress(addressDto);

                // Return success response
                return Ok("Address added successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
*/