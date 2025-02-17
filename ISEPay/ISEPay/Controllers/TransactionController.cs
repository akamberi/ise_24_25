﻿using System.Text.Json;
using ISEPay.BLL.Services.Scoped;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISEPay.Controllers
{
    [ApiController]
    [Route("transactions")]
  //  [Authorize(Policy = "Authenticated")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // 1. Get the last 5 transactions
        [HttpGet("latest")]
        public IActionResult GetLatestTransactions()
        {
            try
            {
                var transactions = _transactionService.GetLatestTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        // 2. Get transactions filtered by date and type
        [HttpGet("filter")]
        public IActionResult GetTransactionsByFilter([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string type)
        {
            try
            {
                var transactions = _transactionService.GetTransactionsByFilter(startDate, endDate, type);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
        
        // 3. Get a transaction by transactionId
        [HttpGet("{transactionId}")]
        public IActionResult GetTransactionById(Guid transactionId)
        {
            try
            {
                var transaction = _transactionService.GetTransactionById(transactionId);
                return Ok(transaction);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
        
        [HttpGet("user/{userId}")]
        public IActionResult GetTransactionByUserId(Guid userId)
        {
            try
            {
                // Merr të dhënat nga shërbimi
                var transactions = _transactionService.GetTransactionsByUserId(userId);

                // Serializimi me ReferenceHandler.Preserve për të shmangur ciklet
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                    WriteIndented = true // Për formatimin e JSON në mënyrë më të lehtë për t'u lexuar
                };

                // Serializo të dhënat dhe kthejme në përgjigje
                var jsonResponse = JsonSerializer.Serialize(transactions, options);

                return Ok(jsonResponse);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
        
        
    }
}
