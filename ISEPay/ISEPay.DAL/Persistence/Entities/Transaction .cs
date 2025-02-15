using System;
using ISEPay.Common;
using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Transaction : BaseEntity<Guid>
    {
        public Guid? AccountInId { get; set; }
        public Account AccountIn { get; set; } 

        public Guid? AccountOutId { get; set; }
        public Account AccountOut { get; set; } 

        public TransactionType Type { get; set; } // Enum for transaction type

       // public Guid? AgentId { get; set; } // Optional Agent's ID

        public decimal? Amount { get; set; } // Transaction amount

        public DateTime Timestamp { get; set; }

        public TransactionStatus Status { get; set; } // Enum for transaction status

        public decimal FeeValue { get; set; } = 0; // Transaction fee (default 0)
        
        public Guid AgentId { get; set; }

        // pritet qe te behen kto 
        //public Guid? ExchangeHistoryId { get; set; }
        //public ExchangeHistory ExchangeHistory { get; set; } // Navigation property for exchange history

        //public Guid? AtmLocationId { get; set; }
        //public AtmLocation AtmLocation { get; set; } // Navigation property for ATM location

        public string Description { get; set; } // Additional transaction details
    }
}
