
using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Account : BaseEntity<Guid>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public AccountStatus Status { get; set; } 
        public AccountType Type { get; set; }   

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> IncomingTransactions { get; set; }
        public ICollection<Transaction> OutgoingTransactions { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
