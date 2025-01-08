using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IAccountRepository : _IBaseRepository<Account, Guid>
    {
        Account? FindAccountById(Guid accountId);
        IEnumerable<Account> FindAccountsByUserId(Guid userId);


        internal class AccountsRepository : _BaseRepository<Account, Guid>, IAccountRepository
        {
            private readonly ISEPayDBContext _context;

            public AccountsRepository(ISEPayDBContext dbContext) : base(dbContext)
            {
                _context = dbContext;
            }

            // Add a new account
            public new void Add(Account entity)
            {
                _context.Accounts.Add(entity);
                _context.SaveChanges();
            }

            // Retrieve all accounts with user information


            // Retrieve an account by ID with user information








            // Retrieve an account by ID
            public Account? FindAccountById(Guid accountId)
            {
                return _context.Accounts
                    .Include(a => a.User)
                    .FirstOrDefault(a => a.Id == accountId);
            }

            // Retrieve accounts by user ID
            public IEnumerable<Account> FindAccountsByUserId(Guid userId)
            {
                return _context.Accounts
                    .Include(a => a.User)
                    .Where(a => a.User.Id == userId)
                    .ToList();
            }

            // Retrieve a single account by city (via user's address)

        }
    }
}
