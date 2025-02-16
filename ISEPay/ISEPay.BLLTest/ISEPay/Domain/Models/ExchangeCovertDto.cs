using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class ExchangeCovertDto
    {

        public string formCurrency {  get; set; }
        public string toCurrency { get; set; }
        public decimal amount { get; set; } 
    }
}
