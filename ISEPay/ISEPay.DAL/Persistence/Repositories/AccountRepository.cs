using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ISEPayDBContext _context;

        public AccountRepository(ISEPayDBContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _context.Accounts
                .Include(a => a.Wallets)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetByUserIdAsync(Guid userId)
        {
            return await _context.Accounts
                .Include(a => a.Wallets)
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .Include(a => a.Wallets)
                .ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await _context.Accounts.AddAsync(account);
        }

        public async Task UpdateAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var existingAccount = await GetByIdAsync(account.Id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException($"Account with ID {account.Id} not found");
            }

            _context.Entry(existingAccount).CurrentValues.SetValues(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await GetByIdAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found");
            }

            _context.Accounts.Remove(account);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Accounts.AnyAsync(a => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}