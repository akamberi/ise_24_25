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
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Globalization;

namespace ISEPay.BLL.Services.Scoped
{
    public interface IAccountService
    {
        void CreateDefaultAccount(Guid userId);
        void ChangeAccountStatus(ChangeAccountStatusRequestDto accountDto);
        void AddAccount(AccountDto account);
        List<AccountResponse> GetUserAccounts(Guid userId);
        void Deposit(DepositRequest depositRequest); 
        void Withdraw(WithdrawalRequest withdrawalRequest); 

        List<AccountResultsDto> SearchAccounts(String fullName, String cardId);


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


        public List<AccountResultsDto> SearchAccounts(string fullName, string cardId)
        {
            var users = usersRepository.FilterByName(fullName).ToList();

            if (!users.Any())
                return new List<AccountResultsDto>(); // No users found, return an empty list

            var accounts = new List<Account>();

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.CardID) && user.CardID.Equals(cardId, StringComparison.OrdinalIgnoreCase))
                {
                    var userAccounts = accountRepository.FindAccountsByUserId(user.Id).ToList();
                    accounts.AddRange(userAccounts);
                }
            }

            return accounts.Select(a => new AccountResultsDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                balance = a.Balance,
                status= a.Status.ToString()
            }).ToList();
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

        public void ChangeAccountStatus(ChangeAccountStatusRequestDto accountDTO)
        {
            // Retrieve all accounts for the provided UserId
            /* var userAccounts = accountRepository.FindAccountsByUserId(accountDTO.UserId).ToList();

             // Check if the user has any accounts
             if (!userAccounts.Any())
             {
                 throw new Exception("No accounts found for this UserId.");
             }
 */
            // Try to find the account directly by AccountNumber
            // var account = userAccounts.FirstOrDefault(a => a.AccountNumber == accountDTO.AccountNumber);

            var account = accountRepository.FindAccountByAccountNumber(accountDTO.AccountNumber);
            // If no account found with the provided AccountNumber
            if (account == null)
            {
                throw new Exception("The provided account number does not exist.");
            }

            if (account.Status.Equals(accountDTO.AccountStatus))
            {
                throw new Exception(" Account is already in this status");
            }

            account.Status = accountDTO.AccountStatus;  
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
            var account = _context.Accounts.FirstOrDefault(a => a.Id == depositRequest.AccountId);

            if (account == null)
            {
                throw new Exception("The account was not found.");
            }

            
            account.Balance += depositRequest.Amount;

            
            var transaction = new Transaction
            {
                AccountInId = depositRequest.AccountId,
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
            
            var account = _context.Accounts.FirstOrDefault(a => a.Id == withdrawalRequest.AccountId);

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
                AccountOutId = withdrawalRequest.AccountId,
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
