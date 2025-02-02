using ISEPay.Common.Enums; // Nevojitet për TransactionType

namespace ISEPay.BLL.ISEPay.Domain.Models
{
    public class FeeModel
    {
        public Guid Id { get; set; }
        
       
        public TransactionType TransactionType { get; set; }

        public decimal FeeValue { get; set; } 
        
        public bool IsActive { get; set; }

        public string Description { get; set; } 

        public decimal Percentage { get; set; } 
        public DateTime EffectiveDate { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}