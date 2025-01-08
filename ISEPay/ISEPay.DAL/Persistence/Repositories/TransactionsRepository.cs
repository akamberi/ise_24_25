using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface ITransactionsRepository : _IBaseRepository<Transaction, Guid>
    {
    }

    internal class TransactionsRepository : _BaseRepository<Transaction, Guid>, ITransactionsRepository
    {
        public TransactionsRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }
    }
}
