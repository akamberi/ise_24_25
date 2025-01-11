using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WalletRepository : IWalletRepository
{
    private readonly ISEPayDBContext _context;

    public WalletRepository(ISEPayDBContext context)
    {
        _context = context;
    }

    public async Task<Wallet> CreateWalletAsync(Wallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId)
    {
        return await _context.Wallets
            .Include(w => w.Account)
            .Where(w => w.Account.UserId == userId)
            .ToListAsync();
    }

    public async Task<Wallet> GetWalletByIdAsync(Guid id)
    {
        return await _context.Wallets
            .Include(w => w.Account)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> HasWalletInCurrencyAsync(Guid accountId, string currency)
    {
        return await _context.Wallets
            .AnyAsync(w => w.AccountId == accountId && w.Currency == currency);
    }
}