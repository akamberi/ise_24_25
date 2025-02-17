using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Entities
{
    public class ExchangeRate : BaseEntity<Guid>
    {
        // From Currency
        public Guid FromCurrencyId { get; set; }
        public Currency? FromCurrency { get; set; }

        // To Currency
        public Guid ToCurrencyId { get; set; }
        public Currency? ToCurrency { get; set; }

        // The exchange rate value
        public decimal Rate { get; set; }

        // Date when the exchange rate was valid
        public DateTime EffectiveDate { get; set; }
    }
}