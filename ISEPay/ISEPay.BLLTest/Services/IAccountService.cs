
using ISEPay.DAL.Persistence.Entities;

public interface IAccountService
{
  
    Task<Account> CreateAccount(Guid userId);
    
    
}