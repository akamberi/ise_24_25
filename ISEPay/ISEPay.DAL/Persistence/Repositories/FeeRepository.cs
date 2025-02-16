using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ISEPay.Common.Enums;

using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IFeeRepository
    {
        void Add(Fee fee);
        Fee GetById(Guid id);
        IEnumerable<Fee> GetAll();
        IEnumerable<Fee> GetFeesByTransactionType(TransactionType transactionType);
        Fee GetFeeByCurrencyPair(Guid fromCurrency, Guid toCurrency, TransactionType transactionType);
        void SaveChanges();
        void Delete(Fee fee);
    }

    public class FeeRepository : IFeeRepository
    {
        private readonly ISEPayDBContext _context;

        public FeeRepository(ISEPayDBContext context)
        {
            _context = context;
        }

        public void Add(Fee fee)
        {
            _context.Fees.Add(fee);
            _context.SaveChanges();
        }

        public Fee GetById(Guid id)
        {
            return _context.Fees.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Fee> GetAll()
        {
            return _context.Fees.ToList();
        }

        public IEnumerable<Fee> GetFeesByTransactionType(TransactionType transactionType)
        {
            return _context.Fees
                .Where(f => f.TransactionType == transactionType && f.IsActive)
                .ToList();
        }
        
        public Fee GetFeeByCurrencyPair(Guid fromCurrency, Guid toCurrency, TransactionType transactionType)
        {
            return _context.Fees
                .Where(f => f.FromCurrency == fromCurrency && f.ToCurrency == toCurrency && f.TransactionType == transactionType && f.IsActive)
                .FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(Fee fee)
        {
            _context.Fees.Remove(fee);
            _context.SaveChanges();
        }
    }
}