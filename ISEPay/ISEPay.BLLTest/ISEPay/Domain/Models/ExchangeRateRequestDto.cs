using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class ExchangeRateRequestDto
    {
        public string FromCurrency { get; set; }


        public string ToCurrency { get; set; }


        public decimal Rate { get; set; }
    }
}
