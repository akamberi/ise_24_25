using Microsoft.AspNetCore.Mvc;
using ISEPay.BLL.Services.Scoped;
using  Microsoft.AspNetCore.Authorization;


namespace ISEPay.Controllers;

[ApiController]
[Route("transactions")]

public class TransactionController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransactionController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost("transfer")]
    [Authorize(Policy = "Authenticated")]
    public IActionResult TransferMoney([FromBody] TransferRequest transferRequest)
    {
        try
        {
            _transferService.TransferMoney(transferRequest);
            return Ok(new { Message = "Successful transfer!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
}