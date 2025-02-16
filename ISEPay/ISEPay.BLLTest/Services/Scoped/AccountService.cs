using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.Common.Enums;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ISEPay.DAL.Persistence;
using ISEPay.Domain.Models;
using Transaction = ISEPay.DAL.Persistence.Entities.Transaction;

namespace ISEPay.BLL.Services.Scoped
{
    public interface IAccountService
    {
        void CreateDefaultAccount(Guid userId);
        void AddAccount(AccountDto account);
        List<AccountResponse> GetUserAccounts(Guid userId);
        void Deposit(DepositRequest depositRequest); 
        void Withdraw(WithdrawalRequest withdrawalRequest); 
    }

    internal class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUsersRepository usersRepository;
        private readonly ISEPayDBContext _context;  

        public AccountService(IAccountRepository accountRepository, IUsersRepository usersRepository, ISEPayDBContext context)
        {
            this.accountRepository = accountRepository;
            this.usersRepository = usersRepository;
            _context = context;  
        }

        public List<AccountResponse> GetUserAccounts(Guid userId)
        {
            var allAccounts = accountRepository.FindAccountsByUserId(userId);

            if (allAccounts == null || !allAccounts.Any())
            {
                throw new Exception("No accounts found for the given user.");
            }

            var accountResponses = allAccounts.Select(account => new AccountResponse
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Currency = account.Currency
            }).ToList();

            return accountResponses;
        }

        public void AddAccount(AccountDto account)
        {
            var user = usersRepository.FindById(account.UserId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            if (user.Status != UserStatus.APPROVED)
            {
                throw new Exception("User is not approved yet");
            }

            var accountToAdd = new Account
            {
                AccountNumber = GenerateAccountNumber(),
                Balance = 0.0m,
                Currency = account.Currency,
                Status = AccountStatus.ACTIVE,
                Type = account.AccountType,
                UserId = account.UserId,
                User = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            accountRepository.Add(accountToAdd);
            accountRepository.SaveChanges();
        }

        public void CreateDefaultAccount(Guid userId)
        {
            var user = usersRepository.FindById(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            var accountToAdd = new Account
            {
                AccountNumber = GenerateAccountNumber(),
                Balance = 0.0m,
                Currency = "ALL",
                Status = AccountStatus.ACTIVE,
                Type = AccountType.STANDARD,
                UserId = userId,
                User = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            accountRepository.Add(accountToAdd);
            accountRepository.SaveChanges();
        }

        private string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper(); 
        }

        
        public void Deposit(DepositRequest depositRequest)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == depositRequest.AccountNumber);

            if (account == null)
            {
                throw new Exception("The account was not found.");
            }

            
            account.Balance += depositRequest.Amount;

            
            var transaction = new Transaction
            {
                AccountInId = depositRequest.AccountNumber,
                AccountIn = account,
                Type = TransactionType.DEPOSIT,
                Amount = depositRequest.Amount,
                Description = "Deposit by agent",
                Status = TransactionStatus.COMPLETED,
                Timestamp = DateTime.Now,
                AgentId = depositRequest.AgentId  
            };

            
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        
        public void Withdraw(WithdrawalRequest withdrawalRequest)
        {
            
            var account = _context.Accounts.FirstOrDefault(a => a.Id == withdrawalRequest.AccountNumber);

            if (account == null)
            {
                throw new Exception("The account was not found.");
            }

            if (account.Balance < withdrawalRequest.Amount)
            {
                throw new Exception("The account balance is insufficient.");
            }

            
            account.Balance -= withdrawalRequest.Amount;

            
            var transaction = new Transaction
            {
                AccountOutId = withdrawalRequest.AccountNumber,
                AccountOut = account,
                Type = TransactionType.WITHDRAWAL,
                Amount = withdrawalRequest.Amount,
                Description = "Withdrawal by agent",
                Status = TransactionStatus.COMPLETED,
                Timestamp = DateTime.Now,
                AgentId = withdrawalRequest.AgentId  
            };

            
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
