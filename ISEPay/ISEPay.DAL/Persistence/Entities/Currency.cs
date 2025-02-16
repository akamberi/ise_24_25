using System;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Currency : BaseEntity<Guid>
    {
        
        /// The ISO 4217 currency code (e.g., USD, EUR).
        public string Code { get; set; }

        /// The full name of the currency (e.g., United States Dollar).

        public string Name { get; set; }

        /// The symbol used to represent the currency (e.g., $, €).

        public string Symbol { get; set; }

        /// Indicates if the currency is currently active.

        public bool IsActive { get; set; }

        /// The country or region where the currency is primarily used.

        public string Country { get; set; }

  
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        // Navigation properties (optional)
        public virtual ICollection<ExchangeRate> FromExchangeRates { get; set; }
        public virtual ICollection<ExchangeRate> ToExchangeRates { get; set; }
    }
}
