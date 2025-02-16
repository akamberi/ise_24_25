namespace ISEPay.BLL.ISEPay.Domain.Models;

public class DepositRequest
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public Guid AgentId { get; set; }
}