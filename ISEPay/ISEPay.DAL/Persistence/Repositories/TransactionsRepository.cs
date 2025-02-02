using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface ITransactionsRepository : _IBaseRepository<Transaction, Guid>
    {
        IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId);
         void AddTransaction(Transaction transaction);
        

    }

    internal class TransactionsRepository : _BaseRepository<Transaction, Guid>, ITransactionsRepository
    {
        private readonly ISEPayDBContext _context;
        
        
        public TransactionsRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        
        
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        
        
        public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId)
        {
            return _context.Transactions
                .Where(t => t.AccountInId == accountId || t.AccountOutId == accountId)
                .Include(t => t.AccountIn)  
                .Include(t => t.AccountOut)
                .ToList();
        }
        
    }
}
