using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Fee
    {
        public Guid Id { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal FeeValue { get; set; }
        
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public decimal Percentage { get; set; }

        public bool IsInternational { get; set; }
        public Guid FromCountryId { get; set; }
        public Guid ToCountryId { get; set; }
        public DateTime EffectiveDate { get; set; }
        
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }
}