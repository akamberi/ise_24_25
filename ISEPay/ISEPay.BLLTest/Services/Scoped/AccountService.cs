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
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

using Transaction = ISEPay.DAL.Persistence.Entities.Transaction;

namespace ISEPay.BLL.Services.Scoped
{

    public interface IAccountService
    {
        void CreateDefaultAccount(Guid userId);
        void DeactivateAccount(DeactivateAccountDto accountDto);
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
            var allAccounts = accountRepository.FindAccountsByUserId(userId)
                                         .Where(account => account.Status == AccountStatus.ACTIVE) // Filtering active accounts
                                         .ToList();

            if (allAccounts == null || !allAccounts.Any())
            {
                throw new Exception("No accounts found for the given user.");
            }

            // Transform the Account entities to AccountResponse objects
            var accountResponses = allAccounts.Select(account => new AccountResponse
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Currency = account.Currency,
                AccountType= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(account.Type.ToString().ToLower())

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
            
            var existingAccount = accountRepository.FindAccountsByUserId(account.UserId)
                .FirstOrDefault(a => a.Currency == account.Currency && a.Type == account.AccountType);
            if (existingAccount != null)
            {
                throw new Exception("Account already exists for this user with the same currency and type");
            }
            var accounts = accountRepository.FindAccountsByUserId(account.UserId);
            var activeAccounts = accounts.Where(account => account.Status.Equals(AccountStatus.ACTIVE)).ToList();
            if (activeAccounts.Count.Equals(5))
            {
                throw new Exception("You cannnot have more tha 5 active accounts");
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

        public void DeactivateAccount(DeactivateAccountDto accountDTO)
        {
            // Retrieve all accounts for the provided UserId
            var userAccounts = accountRepository.FindAccountsByUserId(accountDTO.UserId).ToList();

            // Check if the user has any accounts
            if (!userAccounts.Any())
            {
                throw new Exception("No accounts found for this UserId.");
            }

            // Try to find the account directly by AccountNumber
            var account = userAccounts.FirstOrDefault(a => a.AccountNumber == accountDTO.AccountNumber);

            // If no account found with the provided AccountNumber
            if (account == null)
            {
                throw new Exception("The provided account number does not belong to the provided UserId.");
            }

            // Set the account's status to INACTIVE (or another status for deactivation)
            account.Status = AccountStatus.INACTIVE;  
            account.UpdatedAt = DateTime.UtcNow;

            accountRepository.UpdateAccount(account);  // This method handles saving as well
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
