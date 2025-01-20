using System;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class ExchangeRateDto
    {
        public string FromCurrency { get; set; }

       
        public string ToCurrency { get; set; }

       
        public decimal Rate { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
