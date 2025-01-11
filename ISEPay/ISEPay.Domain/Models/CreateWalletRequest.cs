namespace ISEPay.Domain.Models;

public class CreateWalletRequest
{
    
        public Guid UserId { get; set; }
        public required string Currency { get; set; }
        public decimal InitialBalance { get; set; } 

}