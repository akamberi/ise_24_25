using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using ISEPay.Common.Enums;

namespace ISEPay.BLL.Services
{
    public class FeeService
    {
        private readonly IFeeRepository _feeRepository;

        public FeeService(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;
        }
        

        public void CreateFee(Fee fee)
        {
           
            if (fee.FeeValue <= 0)
            {
                throw new Exception("Fee value must be greater than 0.");
            }

            _feeRepository.Add(fee);
        }

        public Fee GetFeeById(Guid id)
        {
            return _feeRepository.GetById(id);
        }

        public IEnumerable<Fee> GetAllFees()
        {
            return _feeRepository.GetAll();
        }

      
        public Fee GetFeeByTransactionType(TransactionType transactionType, bool isInternational,
            Guid fromCurrency , Guid toCurrency )
        {
            var fees = _feeRepository.GetFeesByTransactionType(transactionType).AsQueryable();

            if (isInternational)
            {
                fees = fees.Where(f => f.Description.Contains("International"));
            }
            else
            {
                fees = fees.Where(f => f.Description.Contains("National"));
            }

            if (fromCurrency != Guid.Empty && toCurrency != Guid.Empty)
            {
                return _feeRepository.GetFeeByCurrencyPair(fromCurrency, toCurrency, transactionType);
            }

            return fees.FirstOrDefault();
        }

        public void UpdateFee(Guid id, Fee updatedFee)
        {
            var existingFee = _feeRepository.GetById(id);
            if (existingFee == null)
            {
                throw new Exception("Fee not found.");
            }

            existingFee.FeeValue = updatedFee.FeeValue;
            existingFee.Description = updatedFee.Description;
            existingFee.TransactionType = updatedFee.TransactionType;
            existingFee.IsActive = updatedFee.IsActive;
            existingFee.EffectiveDate = updatedFee.EffectiveDate;

            _feeRepository.SaveChanges();
        }

        public void DeleteFee(Guid id)
        {
            var fee = _feeRepository.GetById(id);
            if (fee == null)
            {
                throw new Exception("Fee not found.");
            }

            _feeRepository.Delete(fee);
            _feeRepository.SaveChanges();
        }
    }
}