using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.BLL.Services.Scoped;
using ISEPay.Domain.Models; // Assuming ExchangeRateDto is in this namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // For ASP.NET Core MVC or API

namespace ISEPay.Controllers
{
    [Route("exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeRateService exchangeRateService;

        public ExchangeController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpPost()]
        [Authorize(Policy = "Admin")]

        public IActionResult SetExchangeRates([FromBody] ExchangeRateDto exchangeRateDto)
        {
            try
            {
                // Call the service method to set exchange rates
                exchangeRateService.SetExchangeRates(exchangeRateDto);
                return Ok(new { message = "Exchange rates successfully set." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Exchange
        [HttpGet()]
        [Authorize(Policy = "Authenticated")]

        public IActionResult GetExchangeRates()
        {
            try
            {
                // Call the service method to get exchange rates
                var exchangeRates = exchangeRateService.GetExchangeRates();
                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("convert")]
        [Authorize(Policy = "Authenticated")]

        public IActionResult ConvertCurrency([FromBody] ExchangeCovertDto exchangeCovertDto)
        {
            try
            {
                // Call the service method to perform the conversion
                decimal convertedAmount = exchangeRateService.Convert(exchangeCovertDto);
                return Ok(new { convertedAmount });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}
