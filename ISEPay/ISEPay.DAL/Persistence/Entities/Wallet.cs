
using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Wallet : BaseEntity<Guid>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public WalletStatus Status { get; set; } = WalletStatus.ACTIVE;
        public WalletType Type { get; set; } = WalletType.CURRENT;

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
