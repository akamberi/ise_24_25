namespace ISEPay.Domain.Models;

public class Transaction
{
public Guid Id { get; set; }
public Guid AccountId { get; set; }
public decimal Amount { get; set; }
public string TransactionType { get; set; }
public DateTime Date { get; set; }
    
}