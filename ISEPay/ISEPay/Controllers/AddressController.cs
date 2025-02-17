using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services.Scoped;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISEPay.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;
        private readonly IUsersRepository usersRepository;

       
        public AddressController(IAddressService addressService,IUsersRepository usersRepository)
        {
            this.addressService = addressService;
            this.usersRepository = usersRepository;
        }

        [HttpPost]
       // [Authorize(Policy = "Authenticated")]
        public IActionResult AddAddress([FromBody] AddressDto addressDto)
        {
            try
            {
                // Check if the AddressId from the request is valid for this user
                var user = usersRepository.FindById(addressDto.UserId);
                if (user == null)
                {
                    return BadRequest(new { Message = "User does not exist" });
                }

                if (user.AdressID == Guid.Empty)
                {
                    return BadRequest(new { Message = "User does not have a valid AddressId" });
                }

                // Create the address
                addressService.AddAddress(addressDto, user);

                return Ok(new { Message = "Address successfully added!" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error while adding the address", Error = ex.Message });
            } }
       
        [HttpGet("user/{userId}")]
        public IActionResult GetAddressByUserId(Guid userId)
        {
            try
            {
                var address = addressService.GetAddressByUserId(userId);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error while fetching the address", Error = ex.Message });
            }
        }
        
        
        
        [HttpPut("edit-address/{id}")]
        public async Task<IActionResult> EditAddress(Guid id, [FromBody] AddressDto updatedAddressDto)
        { try
            {
                
                addressService.EditAddress(id, updatedAddressDto);

                
                return Ok(new { Message = "Address successfully updated!" });
            }
            catch (ArgumentNullException ex)
            {
                
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error while updating the address", Error = ex.Message });
            }}

    }
}
