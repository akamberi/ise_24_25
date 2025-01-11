using Microsoft.AspNetCore.Mvc;
using ISEPay.BLL.Services;
using ISEPay.Domain.Models;

namespace ISEPay.Controllers;

public class AccountController: ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateWalletRequest request)
    {
        try 
        {
            var account = await _accountService.CreateAccount(request.UserId);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
/*
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetUserAccounts(Guid userId)
    {
        var accounts = await _accountService.GetUserAccounts(userId);
        return Ok(accounts);
    }
    */
}