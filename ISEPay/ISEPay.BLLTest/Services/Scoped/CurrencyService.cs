using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.Domain.Models;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.Services.Scoped
{

    public interface ICurrencyService
    {
        void DeactivateCurrency(string CurrencyCode);
        List<CurrencyDto> GetAllCurrencies();
        List<CurrencyAdminDto> GetALlCurrencyForAdmin();
        void AddCurrency(Currency currency);

    }
    internal class CurrencyService : ICurrencyService
    {

        private readonly ICurrencyRepository currencyRepository;

        public CurrencyService( ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }



        public void DeactivateCurrency(string CurrencyCode) { 
            var currency= currencyRepository.GetByCode(CurrencyCode); 
            Console.WriteLine(currency.ToString());
            if(currency == null )
            {
                throw new Exception("Currency Code does not Exsist" + CurrencyCode);
            }
            currency.IsActive = false;
            currencyRepository.Update(currency);
            currencyRepository.SaveChanges();
        }


        public List<CurrencyDto> GetAllCurrencies()
        {
            // Retrieve all currencies from the repository
            var currencies = currencyRepository.GetAll();

            // Filter only active currencies
            var activeCurrencies = currencies.Where(currency => currency.IsActive).ToList();

            // Map the filtered currencies to CurrencyDto
            var currencyDtos = activeCurrencies.Select(currency => new CurrencyDto
            {
                CurrencyCode = currency.Code,  // Assuming Currency entity has a 'Code' property
                Symbol = currency.Symbol       // Assuming Currency entity has a 'Symbol' property
            }).ToList();

            return currencyDtos;
        }


        public List<CurrencyAdminDto> GetALlCurrencyForAdmin()
        {
            // Retrieve all currencies from the repository
            var currencies = currencyRepository.GetAll();

            // Filter only active currencies
            var activeCurrencies = currencies.Where(currency => currency.IsActive).ToList();

            // Map the filtered currencies to CurrencyDto
            var currencyDtos = activeCurrencies.Select(currency => new CurrencyAdminDto
            {
                CurrencyCode = currency.Code,
                Name=currency.Name  ,
                Country=currency.Country,// Assuming Currency entity has a 'Code' property
                Symbol = currency.Symbol       // Assuming Currency entity has a 'Symbol' property
            }).ToList();

            return currencyDtos;
        }


        public void AddCurrency(Currency currency)
        {
            var exsistingCurrency = currencyRepository.GetByCode(currency.Code);
            if(exsistingCurrency != null)
            {
                throw new Exception("This currency already exsist");
            }

            var newCurrency = new DAL.Persistence.Entities.Currency
            {
                Code = currency.Code,
                Name = currency.Name,
                Symbol = currency.Symbol,
                Country = currency.Country,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add and save the new currency to the repository
            currencyRepository.Add(newCurrency);
            currencyRepository.SaveChanges();
        }

    }
}
