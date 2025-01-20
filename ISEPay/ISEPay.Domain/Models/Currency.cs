using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.Domain.Models
{
    public class Currency
    {


        /// The ISO 4217 currency code (e.g., USD, EUR).
        public string Code { get; set; }

        /// The full name of the currency (e.g., United States Dollar).

        public string Name { get; set; }

        /// The symbol used to represent the currency (e.g., $, €).

        public string Symbol { get; set; }

        public string Country { get; set; }


        
    }
}
