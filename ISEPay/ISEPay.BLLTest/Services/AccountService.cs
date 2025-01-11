using ISEPay.BLL.Services;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;




public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWalletService _walletService;

    public AccountService(IAccountRepository accountRepository, IWalletService walletService)
    {
        _accountRepository = accountRepository;
        _walletService = walletService;
    }

    public async Task<Account> CreateAccount(Guid userId)
    {
        var account = new Account
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _accountRepository.AddAsync(account);
        await _accountRepository.SaveChangesAsync();

        return account;

    }
}