namespace ISEPay.BLL.ISEPay.Domain.Models;

public class DepositRequest
{
    public Guid AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public Guid AgentId { get; set; }
}