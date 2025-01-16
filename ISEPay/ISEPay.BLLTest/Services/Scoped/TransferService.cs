using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.Common.Enums;



namespace ISEPay.BLL.Services.Scoped
{

    public interface ITransferService
    {
        void TransferMoney(TransferRequest transferRequest);
    }

    internal class TransferService : ITransferService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransferService(IAccountRepository accountRepository,ITransactionsRepository transactionsRepository)
        {
            _accountRepository = accountRepository;
            _transactionsRepository=transactionsRepository;
        }

        public void TransferMoney(TransferRequest transferRequest)
        {
            var fromAccount = _accountRepository.FindAccountByAccountNumber(transferRequest.FromAccountNumber);
            var toAccount = _accountRepository.FindAccountByAccountNumber(transferRequest.ToAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                throw new Exception("One of the accounts does not exist");
            }
            
            bool sameUser = fromAccount.UserId == toAccount.UserId;
            
            decimal fee = 0;
            if (!sameUser)
            {
                fee = transferRequest.Amount * 0.02m; 
            }
            
            decimal totalAmount = transferRequest.Amount + fee; 

            

            if (fromAccount.Balance < totalAmount)
            {
                throw new Exception("The balance of the sending account is insufficient");
            }

          
            fromAccount.Balance -= totalAmount;
            toAccount.Balance += transferRequest.Amount;

          
            fromAccount.UpdatedAt = DateTime.UtcNow;
            toAccount.UpdatedAt = DateTime.UtcNow;

         
            _accountRepository.SaveChanges();
            

            var transaction = new Transaction
            {
                AccountInId = fromAccount.Id,
                AccountOutId = toAccount.Id,
                Amount = transferRequest.Amount,
                FeeValue = fee, 
                Type = TransactionType.TRANSFER,
                Status = TransactionStatus.COMPLETED, 
                Timestamp = DateTime.UtcNow,
                Description = sameUser ? "Transfer between the user's accounts" : "Transfer between different users"
            };

            _transactionsRepository.AddTransaction(transaction);

            
        }
    }
    
    
}
