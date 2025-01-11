using Microsoft.AspNetCore.Mvc;
using ISEPay.BLL.Services;
using ISEPay.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ISEPay.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest createWalletRequest)
        {
            if (createWalletRequest == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var wallet = await _walletService.CreateWalletAsync(createWalletRequest);
                return CreatedAtAction(nameof(GetWallet), new { id = wallet.Id }, wallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet(Guid id)
        {
            try
            {
                var wallet = await _walletService.GetWalletByIdAsync(id);
                if (wallet == null)
                {
                    return NotFound("Wallet not found.");
                }
                return Ok(wallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}