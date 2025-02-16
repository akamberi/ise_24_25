namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class WithdrawalRequest
    {
        public Guid AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public Guid AgentId { get; set; }
        
    }
}