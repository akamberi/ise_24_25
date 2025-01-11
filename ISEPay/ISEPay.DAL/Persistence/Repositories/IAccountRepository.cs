
using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Account>> GetAllAsync();
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task SaveChangesAsync();
    }
}