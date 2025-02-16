using ISEPay.BLL.ISEPay.Domain.Models;
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
            try
            {
                
                addressService.AddAddress(addressDto);

                
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
