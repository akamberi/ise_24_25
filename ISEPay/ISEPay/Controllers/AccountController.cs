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

        public AccountController(IAccountService accountService,ISEPayDBContext context)
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
        [Authorize] 
        public IActionResult Deposit([FromBody] DepositRequest depositRequest)
        {
            
            var account = _context.Accounts.FirstOrDefault(a => a.Id == depositRequest.AccountId);

            if (account == null)
            {
                return NotFound("Llogaria nuk u gjet.");
            }

            
            account.Balance += depositRequest.Amount;

            
            var transaction = new ISEPay.DAL.Persistence.Entities.Transaction
            {
                AccountInId = depositRequest.AccountId,
                AccountIn = account,
                Type = TransactionType.DEPOSIT,
                Amount = depositRequest.Amount,
                Description= "Deposit",
                Status = TransactionStatus.COMPLETED,
                Timestamp = DateTime.Now
            };
            _context.Transactions.Add(transaction);

            
            _context.SaveChanges();

            return Ok(new { Message = "Depozita u realizua me sukses!", NewBalance = account.Balance });
        }
        
        
    }
}
