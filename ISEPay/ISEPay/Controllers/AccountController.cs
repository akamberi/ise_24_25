using Microsoft.AspNetCore.Mvc;
using ISEPay.BLL.Services.Scoped;
using ISEPay.BLL.ISEPay.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using ISEPay.DAL.Persistence;
using ISEPay.Common.Enums;


namespace ISEPay.Controllers
{
    [ApiController]
    [Route("accounts/")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ITransferService transferService;
        private readonly ISEPayDBContext _context;

        public AccountController(IAccountService accountService, ISEPayDBContext context, ITransferService transferService)
        {
            this.accountService = accountService;
            this.transferService = transferService;
            _context = context;
        }

        [HttpPost("add")]
        [Authorize(Policy = "Authenticated")]

        public IActionResult AddAccount([FromBody] AccountDto account)
        {
            try
            {
                accountService.AddAccount(account);
                return Ok(new { Message = "Account created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("myAccounts/{userId}")]
        [Authorize(Policy = "Authenticated")]

        public IActionResult GetUserAccounts(Guid userId)
        {
            try
            {
                var accounts = accountService.GetUserAccounts(userId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] DepositRequest depositRequest)
        {
            try
            {
                
                accountService.Deposit(depositRequest);
                return Ok(new { Message = "Deposit successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        
        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] WithdrawalRequest withdrawalRequest)
        {
            try
            {
                
                accountService.Withdraw(withdrawalRequest);
                return Ok(new { Message = "Withdrawal successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        
        [HttpPost("transfer")]
        [Authorize(Policy = "Authenticated")]
        public IActionResult TransferMoney([FromBody] TransferRequest transferRequest)
        {
            try
            {
                transferService.TransferMoney(transferRequest);
                return Ok(new { Message = "Successful transfer!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
          
            }
        }
        
        
        
        
    }
}
