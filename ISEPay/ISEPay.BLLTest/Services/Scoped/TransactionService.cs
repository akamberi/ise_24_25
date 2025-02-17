using ISEPay.Common.Enums;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.Services.Scoped
{
    public interface ITransactionService
    {
        List<Transaction> GetLatestTransactions();
        List<Transaction> GetTransactionsByFilter(DateTime startDate, DateTime endDate, string type);
       
        Transaction GetTransactionById(Guid transactionId);
        List<Transaction> GetTransactionsByUserId(Guid userId);

    }
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        // Get the last 5 transactions
        public List<Transaction> GetLatestTransactions()
        {
            return _transactionsRepository.GetAll()
                .OrderByDescending(t => t.Timestamp)
                .Take(5)
                .ToList();
        }

        // Get transactions filtered by date and type
        public List<Transaction> GetTransactionsByFilter(DateTime startDate, DateTime endDate, string type)
        {
            // Convert string to enum
            if (!Enum.TryParse(type, true, out TransactionType transactionType))
            {
                throw new ArgumentException("Invalid transaction type.");
            }

            return _transactionsRepository.GetAll()
                .Where(t => t.Timestamp >= startDate && t.Timestamp <= endDate && t.Type == transactionType)
                .ToList();
        }
        
        // Get a transaction by its ID
        public Transaction GetTransactionById(Guid transactionId)
        {
            var transaction = _transactionsRepository.GetAll()
                .FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null)
            {
                throw new ArgumentException("Transaction not found.");
            }

            return transaction;
        }
        
        // New method to get transactions by userId
        public List<Transaction> GetTransactionsByUserId(Guid userId)
        {
            return _transactionsRepository.GetTransactionsByUserId(userId).ToList();
        }
    }
}
