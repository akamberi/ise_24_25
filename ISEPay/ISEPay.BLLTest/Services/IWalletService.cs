using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISEPay.Domain.Models;

namespace ISEPay.BLL.Services
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(CreateWalletRequest createWalletRequest);
        Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId);
        Task<Wallet> GetWalletByIdAsync(Guid id);
    }
}