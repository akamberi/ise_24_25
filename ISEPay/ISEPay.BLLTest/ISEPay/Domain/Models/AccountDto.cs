using ISEPay.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class AccountDto
    {

        public Guid UserId { get; set; }
        public string Currency {  get; set; }
        public AccountType AccountType { get; set; }    
    }
}
