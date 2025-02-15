namespace ISEPay.BLL.ISEPay.Domain.Models;

public class DepositRequest
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public Guid AgentId { get; set; }
}