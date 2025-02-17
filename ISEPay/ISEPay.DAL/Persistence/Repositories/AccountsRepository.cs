﻿using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IAccountRepository : _IBaseRepository<Account, Guid>
    {
        Account? FindAccountById(Guid accountId);
        IEnumerable<Account> FindAccountsByUserId(Guid userId);
        Account? FindAccountByAccountNumber(string accountNumber);
        void UpdateAccounts(IEnumerable<Account> accounts); // Add this method signature
        void UpdateAccount(Account account); // New method signature
        
    }

    internal class AccountsRepository : _BaseRepository<Account, Guid>, IAccountRepository
    {
        private readonly ISEPayDBContext _context;

        public AccountsRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

  
        public Account? FindAccountByAccountNumber(string accountNumber)
        {
            Console.WriteLine($"Searching for account with AccountNumber: {accountNumber}");
            return _context.Accounts
                .Include(a => a.User)
                .FirstOrDefault(a => a.AccountNumber == accountNumber); // Assuming AccountNumber is a property of Account entity
       
        }

            // Add a new account
            public new void Add(Account entity)
            {
                _context.Accounts.Add(entity);
                _context.SaveChanges();
            }
            
            // Retrieve an account by ID
            public Account? FindAccountById(Guid accountId)
            {
                return _context.Accounts
                    .Include(a => a.User)
                    .FirstOrDefault(a => a.Id == accountId);
            }

        // Retrieve accounts by user ID
        public IEnumerable<Account> FindAccountsByUserId(Guid userId)
        {
            return _context.Accounts
                .Include(a => a.User)
                .Where(a => a.User.Id == userId)
                .ToList();
        }

        // Update multiple accounts
        public void UpdateAccounts(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                _context.Entry(account).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            // Attach the entity if it's not already tracked by the context
            _context.Entry(account).State = EntityState.Modified;
            _context.SaveChanges();
        }

        }
    }
