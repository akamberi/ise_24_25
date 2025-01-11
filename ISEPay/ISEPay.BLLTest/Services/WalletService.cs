using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.BLL.Services;
using ISEPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IAccountRepository _accountRepository;

    public WalletService(IWalletRepository walletRepository, IAccountRepository accountRepository)
    {
        _walletRepository = walletRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Wallet> CreateWalletAsync(CreateWalletRequest createWalletRequest)
    {
        var account = await _accountRepository.GetByUserIdAsync(createWalletRequest.UserId);
        if (account == null)
        {
            throw new KeyNotFoundException("Account not found");
        }

        var hasWallet = await _walletRepository.HasWalletInCurrencyAsync(account.Id, createWalletRequest.Currency);
        if (hasWallet)
        {
            throw new InvalidOperationException($"Wallet with currency {createWalletRequest.Currency} already exists for this account");
        }

        var wallet = new Wallet
        {
            AccountId = account.Id,
            Currency = createWalletRequest.Currency,
            Balance = createWalletRequest.InitialBalance,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _walletRepository.CreateWalletAsync(wallet); 
        return wallet;
    }

    public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId)
    {
        return await _walletRepository.GetWalletsByUserIdAsync(userId);
    }

    public async Task<Wallet> GetWalletByIdAsync(Guid id)
    {
        var wallet = await _walletRepository.GetWalletByIdAsync(id);
        if (wallet == null)
        {
            throw new KeyNotFoundException("Wallet not found");
        }
        return wallet;
    }
}
