using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class AccountResultsDto
    {

        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal balance { get; set; }
    }
}
