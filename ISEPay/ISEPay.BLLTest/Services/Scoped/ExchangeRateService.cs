using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ISEPay.BLL.Services.Scoped
{

    public interface IExchangeRateService
    {
        void SetExchangeRates(ExchangeRateRequestDto exchangeRateDto);
        List<ExchangeRateDto> GetExchangeRates();

        decimal Convert(ExchangeCovertDto exchangeCovertDto);


    }
    internal class ExchangeRateService : IExchangeRateService
    {

        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly ICurrencyRepository currencyRepository;


        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository, ICurrencyRepository currencyRepository) {
           this.exchangeRateRepository = exchangeRateRepository;
            this.currencyRepository = currencyRepository;
        }

        public void SetExchangeRates(ExchangeRateRequestDto exchangeRateDto)
        {
            // Retrieve currencies from the repository
            var fromCurrency = currencyRepository.GetByCode(exchangeRateDto.FromCurrency);
            var toCurrency = currencyRepository.GetByCode(exchangeRateDto.ToCurrency);

            // Validate that the currencies exist
            if (fromCurrency == null)
            {
                throw new Exception("Currency Code does not exist: " + exchangeRateDto.FromCurrency);
            }

            if (toCurrency == null)
            {
                throw new Exception("Currency Code does not exist: " + exchangeRateDto.ToCurrency);
            }

            // Check if the exchange rate already exists
            var existingRate = exchangeRateRepository.GetExchangeRate(fromCurrency.Id, toCurrency.Id);

            if (existingRate != null)
            {
                // Update the existing exchange rate
                existingRate.Rate = exchangeRateDto.Rate;
                existingRate.EffectiveDate = DateTime.Now;
                exchangeRateRepository.Update(existingRate);
                exchangeRateRepository.SaveChanges(); // Assuming an Update method exists
            }
            else
            {
                // Add a new exchange rate
                var newRate = new ExchangeRate
                {
                    FromCurrencyId = fromCurrency.Id,
                    ToCurrencyId = toCurrency.Id,
                    Rate = exchangeRateDto.Rate,
                    EffectiveDate = DateTime.Now
                };

                exchangeRateRepository.Add(newRate);
                exchangeRateRepository.SaveChanges();// Assuming an Add method exists
            }
        }
        public List<ExchangeRateDto> GetExchangeRates()
        {
            // Retrieve all exchange rates from the repository
            var exchangeRates = exchangeRateRepository.GetAllExchangeRates();

            // Retrieve all currencies once and store them in a dictionary for quick lookup by ID
            var currencies = currencyRepository.GetAll() // Assume this gets all currencies
                .ToDictionary(c => c.Id, c => c); // Map currency ID to the Currency entity

            // Map the retrieved exchange rates to ExchangeRateDto
            var exchangeRateDtos = exchangeRates.Select(rate => new ExchangeRateDto
            {
                FromCurrency = currencies.ContainsKey(rate.FromCurrencyId)
                    ? currencies[rate.FromCurrencyId]?.Code // Use currency code if found
                    : "Unknown",  // Fallback if currency not found
                ToCurrency = currencies.ContainsKey(rate.ToCurrencyId)
                    ? currencies[rate.ToCurrencyId]?.Code // Use currency code if found
                    : "Unknown",  // Fallback if currency not found
                Rate = rate.Rate,
                LastUpdated = rate.EffectiveDate
            }).ToList();

            return exchangeRateDtos;
        }





        public decimal Convert(ExchangeCovertDto exchangeCovertDto)
        {
            // Retrieve the currencies based on the provided codes
            var fromCurrency = currencyRepository.GetByCode(exchangeCovertDto.formCurrency);
            var toCurrency = currencyRepository.GetByCode(exchangeCovertDto.toCurrency);

            // Validate that both currencies exist
            if (fromCurrency == null)
            {
                throw new Exception($"Currency with code {exchangeCovertDto.formCurrency} does not exist.");
            }

            if (toCurrency == null)
            {
                throw new Exception($"Currency with code {exchangeCovertDto.toCurrency} does not exist.");
            }

            // Retrieve the exchange rate from the repository
            var exchangeRate = exchangeRateRepository.GetExchangeRate(fromCurrency.Id, toCurrency.Id);

            if (exchangeRate == null)
            {
                throw new Exception($"Exchange rate from {exchangeCovertDto.formCurrency} to {exchangeCovertDto.toCurrency} not found.");
            }

            // Perform the conversion
            decimal convertedAmount = exchangeCovertDto.amount * exchangeRate.Rate;

            return convertedAmount;
        }


    }
}
