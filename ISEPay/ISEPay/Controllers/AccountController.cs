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
        private readonly ISEPayDBContext _context;

        public AccountController(IAccountService accountService, ISEPayDBContext context)
        {
            this.accountService = accountService;
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

        [HttpPut("status")]
        [Authorize(Policy = "Agent")]
        public IActionResult ChangeStatus([FromBody] ChangeAccountStatusRequestDto accountDto)
        {
            try
            {
                // Call the service layer to deactivate the account
                accountService.ChangeAccountStatus(accountDto);

                // Return success response
                return Ok(new { Message = "Account status changed successfully:  " + accountDto.AccountStatus.ToString() });
            }
            catch (Exception ex)
            {
                // Return error response
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

        [HttpGet("search")]
        [Authorize(Policy = "Agent")]
        public IActionResult SearchAccounts([FromQuery] string fullName, [FromQuery] string cardId)
        {
            try
            {
                var accounts = accountService.SearchAccounts(fullName, cardId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
