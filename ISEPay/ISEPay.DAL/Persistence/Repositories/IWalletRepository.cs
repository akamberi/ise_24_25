// IAccountRepository.cs

using ISEPay.DAL.Persistence.Entities;
public interface IWalletRepository 
{
    Task<Wallet> CreateWalletAsync(Wallet wallet);
    Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId);
    Task<Wallet> GetWalletByIdAsync(Guid id);
    
    Task<bool> HasWalletInCurrencyAsync(Guid userId, string currency);
}