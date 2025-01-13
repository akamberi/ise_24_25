using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface ICurrencyRepository : _IBaseRepository<Currency, Guid>
    {
        /// <summary>
        /// Gets a currency by its code.
        /// </summary>
        Currency GetByCode(string code);

        /// <summary>
        /// Retrieves all active currencies.
        /// </summary>
        IEnumerable<Currency> GetAllActiveCurrencies();

        /// <summary>
        /// Checks if a currency with the specified code exists.
        /// </summary>
        bool ExistsByCode(string code);

        /// <summary>
        /// Updates an existing currency.
        /// </summary>
        void Update(Currency currency);
    }

    internal class CurrencyRepository : _BaseRepository<Currency, Guid>, ICurrencyRepository
    {
        public CurrencyRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets a currency by its code.
        /// </summary>
        public Currency GetByCode(string code)
        {
            return _dbContext.Set<Currency>()
                .FirstOrDefault(c => c.Code == code);
        }

        /// <summary>
        /// Retrieves all active currencies.
        /// </summary>
        public IEnumerable<Currency> GetAllActiveCurrencies()
        {
            return _dbContext.Set<Currency>()
                .Where(c => c.IsActive)
                .ToList();
        }

        /// <summary>
        /// Checks if a currency with the specified code exists.
        /// </summary>
        public bool ExistsByCode(string code)
        {
            return _dbContext.Set<Currency>()
                .Any(c => c.Code == code);
        }

        /// <summary>
        /// Updates an existing currency.
        /// </summary>
        public void Update(Currency currency)
        {
            // Attach the entity to the context (if it is not already tracked)
            _dbContext.Set<Currency>().Update(currency);

            // Alternatively, you can use the following if you want to make sure the entity is attached first
            // var existingCurrency = _dbContext.Set<Currency>().Find(currency.Id);
            // if (existingCurrency != null)
            // {
            //     _dbContext.Entry(existingCurrency).CurrentValues.SetValues(currency);
            // }

            // Mark the entity for update
            _dbContext.SaveChanges();
        }
    }
}
