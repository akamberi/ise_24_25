using Microsoft.AspNetCore.Mvc;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.BLL.Services;
using ISEPay.Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ISEPay.Controllers;


[Route("api/[controller]")]
[ApiController]
public class FeeController : ControllerBase
{
    private readonly FeeService _feeService;
    
    public FeeController(FeeService feeService)
    {
        _feeService = feeService;
    }
    
        // GET: api/Fee
        [HttpGet]
        public ActionResult<IEnumerable<Fee>> GetFees()
        {
            try
            {
                var fees = _feeService.GetAllFees();
                return Ok(fees); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/Fee/transfer
        [HttpGet("{transactionType}")]
        public ActionResult<Fee> GetFeesByTransactionType(TransactionType transactionType, [FromQuery] bool isInternational,Guid fromCurrency, Guid toCurrency)
        {
            try
            {
                var fee = _feeService.GetFeeByTransactionType(transactionType, isInternational, fromCurrency, toCurrency);
        
                if (fee == null)
                {
                    return NotFound("Fee not found for this transaction type.");
                }

                return Ok(fee); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST: api/Fee
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Fee> CreateFee([FromBody] Fee fee)
        {
            try
            {
                if (fee == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                _feeService.CreateFee(fee);
                return CreatedAtAction("GetFees", new { id = fee.Id }, fee); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT: api/Fee/{id}
         [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateFee(Guid id, [FromBody] Fee fee)
        {
            try
            {
                if (fee == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                var existingFee = _feeService.GetFeeById(id);
                if (existingFee == null)
                {
                    return NotFound("Fee not found.");
                }

                _feeService.UpdateFee(id, fee);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE: api/Fee/{id}
        
         [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteFee(Guid id)
        {
            try
            {
                var existingFee = _feeService.GetFeeById(id);
                if (existingFee == null)
                {
                    return NotFound("Fee not found.");
                }

                _feeService.DeleteFee(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
    
