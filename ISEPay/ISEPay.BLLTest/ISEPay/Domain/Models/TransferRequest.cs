using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;





public class TransferRequest
{
    public string FromAccountNumber { get; set; }  
    public string ToAccountNumber { get; set; }    
    public decimal Amount { get; set; }  
    public Guid FromCurrency { get; set; }
    public Guid ToCurrency { get; set; }
    
    public string FromCountry { get; set; } 
    public string ToCountry { get; set; } 
}
