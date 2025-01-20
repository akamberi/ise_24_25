using ISEPay.BLL.Services.Scoped;
using ISEPay.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISEPay.Controllers
{
    [Route("currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        // Constructor with dependency injection
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Retrieves all active currencies.
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "Authenticated")]

        public IActionResult GetAllCurrencies()
        {
            try
            {
                var currencies = _currencyService.GetAllCurrencies();
                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while fetching currencies." });
            }
        }

        /// <summary>
        /// Deactivates a currency by its code.
        /// </summary>
        [HttpPost("deactivate/{currencyCode}")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeactivateCurrency(string currencyCode)
        {
            try
            {
                _currencyService.DeactivateCurrency(currencyCode);
                return Ok(new { message = $"Currency {currencyCode} deactivated successfully." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = $"Failed to deactivate the currency with code {currencyCode}." });
            }
        }

        /// <summary>
        /// Adds a new currency.
        /// </summary>
        [HttpPost("add")]
        [Authorize(Policy = "Admin")]
        public IActionResult AddCurrency([FromBody] Currency currency)
        {
            if (currency == null || string.IsNullOrEmpty(currency.Code))
            {
                return BadRequest(new { message = "Currency data is invalid or incomplete." });
            }

            try
            {
                _currencyService.AddCurrency(currency);
                return Ok(new { message = $"Currency {currency.Code} added successfully." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = $"Failed to add the currency with code {currency.Code}." });
            }
        }
    }
}
