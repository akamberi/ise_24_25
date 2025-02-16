using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IExchangeRateRepository : _IBaseRepository<ExchangeRate, Guid>
    {
        ExchangeRate GetExchangeRate(Guid fromCurrencyId, Guid toCurrencyId);
        IEnumerable<ExchangeRate> GetAllExchangeRates();
        void Update(ExchangeRate exchangeRate); // Method to update the exchange rate
    }

    internal class ExchangeRateRepository : _BaseRepository<ExchangeRate, Guid>, IExchangeRateRepository
    {
        public ExchangeRateRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }

        // Get the exchange rate between two currencies
        public ExchangeRate GetExchangeRate(Guid fromCurrencyId, Guid toCurrencyId)
        {
            return _dbContext.ExchangeRates
                .FirstOrDefault(er => er.FromCurrencyId == fromCurrencyId && er.ToCurrencyId == toCurrencyId);
        }

        // Get all exchange rates
        public IEnumerable<ExchangeRate> GetAllExchangeRates()
        {
            return _dbContext.ExchangeRates.ToList();
        }

        // Update the exchange rate for a given pair of currencies
        public void Update(ExchangeRate exchangeRate)
        {
            // Find the existing exchange rate by its Id
            var existingExchangeRate = _dbContext.ExchangeRates
                .FirstOrDefault(er => er.Id == exchangeRate.Id);

            if (existingExchangeRate != null)
            {
                // Update the properties
                existingExchangeRate.FromCurrencyId = exchangeRate.FromCurrencyId;
                existingExchangeRate.ToCurrencyId = exchangeRate.ToCurrencyId;
                existingExchangeRate.Rate = exchangeRate.Rate;
                existingExchangeRate.EffectiveDate = exchangeRate.EffectiveDate;

                // Save the changes
                _dbContext.SaveChanges();
            }
            else
            {
                // Handle the case where the exchange rate does not exist
                throw new Exception("Exchange rate not found for the specified Id.");
            }
        }
    }
}
