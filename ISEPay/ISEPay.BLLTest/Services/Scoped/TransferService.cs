using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.Common.Enums;





namespace ISEPay.BLL.Services.Scoped
{

    public interface ITransferService
    {
        void TransferMoney(TransferRequest transferRequest);
        Fee GetFeeByCurrencyPair(TransactionType transactionType,bool isInternational,Guid fromCurrency,Guid toCurrency);
    }

    internal class TransferService : ITransferService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionsRepository _transactionsRepository;
         private readonly IExchangeRateRepository _exchangeRateRepository;
         private readonly IFeeRepository _feeRepository;
         private readonly IAddressRepository _addressRepository;
         private readonly FeeService _feeService;



        public TransferService(IAccountRepository accountRepository,ITransactionsRepository transactionsRepository,
            IExchangeRateRepository exchangeRateRepository,IAddressRepository addressRepository,FeeService feeService,IFeeRepository feeRepository)
        {
            _accountRepository = accountRepository;
            _transactionsRepository=transactionsRepository;
            _exchangeRateRepository=exchangeRateRepository;
            _feeRepository=feeRepository;
            _addressRepository = addressRepository;
            _feeService=feeService;
        }

        
        public Fee GetFeeByCurrencyPair(TransactionType transactionType, bool isInternational,
            Guid fromCurrency, Guid toCurrency)
        {
           

            if (isInternational)
            {
                return _feeRepository.GetFeeByCurrencyPair(fromCurrency, toCurrency, transactionType);
            }
            else
            {
                return _feeRepository.GetFeeByCurrencyPair(fromCurrency, toCurrency, transactionType);
            }
        }

        public void TransferMoney(TransferRequest transferRequest)
        {
            var fromAccount = _accountRepository.FindAccountByAccountNumber(transferRequest.FromAccountNumber);
            var toAccount = _accountRepository.FindAccountByAccountNumber(transferRequest.ToAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                throw new Exception("One of the accounts does not exist");
            }

            var fromAddress = _addressRepository.GetByUserId(fromAccount.UserId);
            var toAddress = _addressRepository.GetByUserId(toAccount.UserId);

            if (fromAddress == null || toAddress == null)
            {
                throw new Exception("Address for one of the accounts not found ");
            }
            
            
            bool sameUser = fromAccount.UserId == toAccount.UserId;
            bool isInternational = fromAddress.Country != toAddress.Country; 
            
            decimal fee = 0;
            
            if (!sameUser)
            {
                var feeObj = _feeService.GetFeeByTransactionType(
                    TransactionType.TRANSFER,
                    isInternational,
                    Guid.Parse(fromAccount.Currency),  
                    Guid.Parse(toAccount.Currency)     
                );
                fee = feeObj?.FeeValue ?? 0m;
            }
            
            decimal totalAmount = transferRequest.Amount + fee; 
            
            if (fromAccount.Balance < totalAmount)
            {
                throw new Exception("The balance of the sending account is insufficient");
            }

                   decimal exchangeRate = 1m;
                   
                   var fromCurrency = Guid.Parse(fromAccount.Currency);
                   var toCurrency = Guid.Parse(toAccount.Currency);

                     if (fromCurrency != toCurrency) 
                    {
                        var rate = _exchangeRateRepository.GetExchangeRate(fromCurrency, toCurrency); 
                        if (rate == null)
                        {
                            throw new Exception("Exchange rate not found");
                        }

                        exchangeRate = rate.Rate;
                        totalAmount *= exchangeRate; 
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
