namespace ISEPay.DAL.Persistence.Entities;

public class Account :BaseEntity<Guid>
{

    public Guid UserId { get; set; }
    public User User { get; set; }

    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    
}