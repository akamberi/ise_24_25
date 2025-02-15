namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class WithdrawalRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public Guid AgentId { get; set; }
        
    }
}